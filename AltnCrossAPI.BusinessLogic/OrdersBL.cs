using altn_common.KeyCodes;
using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using AltnCrossAPI.Shared;
using Newtonsoft.Json;
using AltnCrossAPI.Shared.Logging;
using ShopifySharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static AltnCrossAPI.Shared.Enums;
using altn_common.Profiles;

namespace AltnCrossAPI.BusinessLogic
{
    public class OrdersBL : IOrdersBL
    {
        private readonly OrderService orderService = new OrderService(ConfigHelper.ShopifyUrl, ConfigHelper.ShopAccessToken);
        private readonly FulfillmentService fulfillmentService = new FulfillmentService(ConfigHelper.ShopifyUrl, ConfigHelper.ShopAccessToken);
        private readonly TransactionService transactionService = new TransactionService(ConfigHelper.ShopifyUrl, ConfigHelper.ShopAccessToken);
        private ILogger _logger;
        private IShopifyOrders _order;
        private IUsers _user;
        private IShopifyData _shopifyData;
        private IShopifyOrderAddresses _address;
        private ICustomersBL _customerBL;
        private IShopifyOrderLineItems _lineItem;
        private IRegKeys _regKey;

        public OrdersBL(IShopifyOrders order,
                        IShopifyData shopifyData,
                        IUsers user,
                        IShopifyOrderLineItems lineItems,
                        ICustomersBL customerBL,
                        IShopifyOrderAddresses addresses,
                        IRegKeys regKey,
            ILogger logger)
        {
            _order = order;
            _shopifyData = shopifyData;
            _customerBL = customerBL;
            _address = addresses;
            _lineItem = lineItems;
            _regKey = regKey;
            _user = user;
            _logger = logger;
        }


        /// <summary>
        /// Inserts a new shopify order or Update existing shopify order in db
        /// and mark it complete after adding keys to shopify order NoteAttribute
        /// </summary>
        /// <param name="order">ShopifySharp Order posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        public async Task<string> OrderSync(Order order)
        {
            try
            {
                //Adding logs of the json posted from shopify to the database 
                ShopifyDataModel sData = new ShopifyDataModel();
                sData.JSON = JsonConvert.SerializeObject(order);
                sData.EventType = "Insert";//By default insert. Updated via ShopifyOrder Insert/Updated
                sData.Entity = "Order";
                sData.DateAdded = DateTime.Now;
                int shopifyDataId = _shopifyData.ShopifyDataInsert(sData);

                var transactions = await transactionService.ListAsync(order.Id ?? 0);
                bool paymentRequired = !(transactions.Any(t => t.Gateway.ToLower().Contains("wire transfer") || t.Gateway.ToLower().Contains("po") || t.Gateway.ToLower().Contains("payment on account")));
                if ((transactions.Any(t => t.Gateway.ToLower().Contains("wire transfer") || t.Gateway.ToLower().Contains("po"))) && !order.Tags.Contains("Payment Received"))
                {
                    //add tag to shopify order
                    var orderUpdated = await orderService.UpdateAsync(order.Id ?? 0, new Order()
                    {
                        Tags = "PO/Wire Transfer"
                    });
                    _logger.Info(ToStringHttpCustomResponse(OrderHttpCustomResponse.POWireTransferPaymentsError));
                    return ToStringHttpCustomResponse(OrderHttpCustomResponse.POWireTransferPaymentsError);
                }
                if (order.FinancialStatus.ToLower() == "paid" || !paymentRequired)
                {

                    //Insert or Update order in database as per the json received from shopify
                    ShopifyOrderModel sOrder = new ShopifyOrderModel();
                    sOrder.ShopifyId = order.Id ?? 0;
                    sOrder.OrderNumber = order.OrderNumber ?? 0;
                    sOrder.ShopifyDataId = shopifyDataId;
                    sOrder.Email = order.Email ?? "";
                    sOrder.CreatedOn = order.CreatedAt.Value.UtcDateTime;
                    sOrder.UpdatedOn = order.UpdatedAt.Value.UtcDateTime;
                    sOrder.ProcessedOn = order.ProcessedAt.Value.UtcDateTime;
                    sOrder.Token = order.Token ?? "";
                    sOrder.CheckoutToken = order.CartToken ?? "";
                    sOrder.Gateway = order.PaymentGatewayNames.ToString();
                    sOrder.TotalPrice = order.TotalPrice ?? 0;
                    sOrder.TotalDiscount = order.TotalDiscounts ?? 0;
                    sOrder.SubTotalPrice = order.SubtotalPrice ?? 0;
                    sOrder.TotalTax = order.TotalTax ?? 0;
                    sOrder.FinancialStatus = order.FinancialStatus;
                    sOrder.ProcessingMethod = order.ProcessingMethod ?? "";
                    sOrder.Currency = order.Currency ?? "";
                    sOrder.CheckoutId = long.MinValue;
                    sOrder.AppId = order.AppId ?? 0;
                    sOrder.BrowserIP = order.BrowserIp ?? "";
                    sOrder.OrderStatusUrl = order.OrderStatusUrl ?? "";
                    _order.ShopifyOrderInsertUpdate(sOrder);


                    long[] shopifyIds = new long[order.LineItems.Count()];
                    int index = 0;
                    //Insert or Update order line items in database
                    foreach (LineItem item in order.LineItems)
                    {
                        ShopifyOrderLineItemModel sOrderLineItems = new ShopifyOrderLineItemModel();
                        sOrderLineItems.ShopifyId = item.Id ?? 0;
                        sOrderLineItems.OrderId = order.Id ?? 0;
                        sOrderLineItems.VariantId = item.VariantId ?? 0;
                        sOrderLineItems.Title = item.Title;
                        sOrderLineItems.Quantity = item.Quantity ?? 1;
                        sOrderLineItems.FulfillableQuantity = item.FulfillableQuantity ?? 0;
                        sOrderLineItems.SKU = item.SKU;
                        sOrderLineItems.Vendor = item.Vendor;
                        sOrderLineItems.ProductId = item.ProductId ?? 0;
                        sOrderLineItems.Price = item.Price ?? 0;
                        sOrderLineItems.TotalDiscount = item.TotalDiscount ?? 0;
                        //sOrderLineItems.TaxCode = item.ta
                        _lineItem.ShopifyOrderLineItemInsertUpdate(sOrderLineItems);
                        shopifyIds[index] = item.Id ?? 0;
                        index++;
                    }
                    //delete order line items in database which are not part of shopify json now
                    if (shopifyIds.Length > 0)
                    {
                        string lineItemDeleteWhereClause = " WHERE ShopifyId not in (@ShopifyId) and OrderId = " + order.Id;
                        lineItemDeleteWhereClause = lineItemDeleteWhereClause.Replace("@ShopifyId", string.Join(",", shopifyIds));
                        _lineItem.ShopifyOrderLineItemsDelete(lineItemDeleteWhereClause);
                    }

                    if (order.BillingAddress != null && order.BillingAddress.Address1 != null)
                    {
                        //Insert or Update order billing address in database
                        ShopifyOrderAddressModel sOrderAddress = new ShopifyOrderAddressModel();
                        sOrderAddress.FirstName = order.BillingAddress.FirstName;
                        sOrderAddress.LastName = order.BillingAddress.LastName;
                        sOrderAddress.Address = order.BillingAddress.Address1 + ";" + order.BillingAddress.Address2;
                        sOrderAddress.Phone = order.BillingAddress.Phone;
                        sOrderAddress.City = order.BillingAddress.City;
                        sOrderAddress.Zip = order.BillingAddress.Zip;
                        sOrderAddress.Province = order.BillingAddress.Province;
                        sOrderAddress.Country = order.BillingAddress.Country;
                        sOrderAddress.Latitude = order.BillingAddress.Latitude ?? 0;
                        sOrderAddress.Longitude = order.BillingAddress.Longitude ?? 0;
                        sOrderAddress.CountryCode = order.BillingAddress.CountryCode;
                        sOrderAddress.AddressType = ShopifyOrderAddressModel.AddressesType.Billing;
                        sOrderAddress.OrderId = order.Id ?? 0;
                        _address.ShopifyOrderAddressInsertUpdate(sOrderAddress);
                    }
                    else
                    {
                        //delete order business address in database which are not part of shopify json now
                        string lineItemDeleteWhereClause = " WHERE AddressType = " + (int)ShopifyOrderAddressModel.AddressesType.Billing + " and OrderId = " + order.Id;
                        _address.ShopifyOrderAddresssesDelete(lineItemDeleteWhereClause);
                    }

                    if (order.ShippingAddress != null && order.ShippingAddress.Address1 != null)
                    {
                        //Insert or Update order shipping address in database
                        ShopifyOrderAddressModel sOrderAddress = new ShopifyOrderAddressModel();
                        sOrderAddress.FirstName = order.ShippingAddress.FirstName;
                        sOrderAddress.LastName = order.ShippingAddress.LastName;
                        sOrderAddress.Address = order.ShippingAddress.Address1 + ";" + order.ShippingAddress.Address2;
                        sOrderAddress.Phone = order.ShippingAddress.Phone;
                        sOrderAddress.City = order.ShippingAddress.City;
                        sOrderAddress.Zip = order.ShippingAddress.Zip;
                        sOrderAddress.Province = order.ShippingAddress.Province;
                        sOrderAddress.Country = order.ShippingAddress.Country;
                        sOrderAddress.Latitude = order.ShippingAddress.Latitude ?? 0;
                        sOrderAddress.Longitude = order.ShippingAddress.Longitude ?? 0;
                        sOrderAddress.CountryCode = order.ShippingAddress.CountryCode;
                        sOrderAddress.AddressType = ShopifyOrderAddressModel.AddressesType.Shipping;
                        sOrderAddress.OrderId = order.Id ?? 0;
                        _address.ShopifyOrderAddressInsertUpdate(sOrderAddress);
                    }
                    else
                    {
                        //delete order shipping address in database which are not part of shopify json now
                        string lineItemDeleteWhereClause = " WHERE AddressType = " + (int)ShopifyOrderAddressModel.AddressesType.Shipping + " and OrderId = " + order.Id;
                        _address.ShopifyOrderAddresssesDelete(lineItemDeleteWhereClause);
                    }

                    return await CompleteOrder(order);
                }
                else
                {
                    _logger.Info(ToStringHttpCustomResponse(OrderHttpCustomResponse.PaymentError));
                    return ToStringHttpCustomResponse(OrderHttpCustomResponse.PaymentError);
                }
            }
            catch (Exception exp)
            {
                _logger.Error("OrderSync", exp);
                return HttpStatusCode.InternalServerError.ToString();
            }
        }

        /// <summary>
        /// Completes order in Shopify after getting/generating keys from database 
        /// and adding it to shopify order NoteAttribute
        /// </summary>
        /// <param name="order">ShopifySharp Order from Shopify</param>
        /// <returns>Returns status after processing the order</returns>
        private async Task<string> CompleteOrder(Order order)
        {
            try
            {
                List<NoteAttribute> noteAttributes = new List<NoteAttribute>();
                var customer = _user.UserGetByShopifyCustomerID(order.Customer.Id);
                if(!customer.IsPopulated)//Customer does not exist. Add it.
                {
                    _customerBL.CustomerSync(order.Customer);
                }
                string userId = new UserProfile(order.Customer.Email).UserID;
                string userEmail = order.Customer.Email;

                //Get/Generate Keys for every Line Items
                foreach (LineItem item in order.LineItems)
                {
                    ProductSku sku = new ProductSku(item.SKU);

                    RegKeyModel regKeyString = new RegKeyModel();
                    regKeyString.SKU = item.SKU;
                    regKeyString.ProductSize = item.Quantity ?? 1;
                    regKeyString.UserID = userId;
                    regKeyString.Username = userEmail;
                    string keyString = _regKey.RegKeyStringGet(regKeyString);

                    //Key does not exists already in db. let's generate it
                    if (keyString == "")
                    {
                        var licenseTerm = item.Properties.Any(p => p.Name.ToString() == "licence_Term") ? int.Parse(item.Properties.FirstOrDefault(p => p.Name.ToString() == "licence_Term").Value.ToString()) : 1;
                        RegKey regKey = new RegKey("", userId, userEmail, null, "", sku, sku.ProductCode, ProductType.PRO, item.Quantity ?? 1, RegKeyStatus.Purchased, DateTime.Now, DateTime.Now.AddYears(licenseTerm), true);
                        regKey.Save("ShopifyAPI", KeyChangeMethod.PURCHASE, "");
                        noteAttributes.Add(new NoteAttribute { Name = item.Id.ToString(), Value = regKey.KeyString });
                    }
                    else
                    {
                        noteAttributes.Add(new NoteAttribute { Name = item.Id.ToString(), Value = keyString });
                    }
                }
                //Check if any key retrieved from db
                if (noteAttributes.Count() > 0)
                {
                    //add keys to shopify order
                    var orderUpdated = await orderService.UpdateAsync(order.Id ?? 0, new Order()
                    {
                        NoteAttributes = noteAttributes
                    });

                    //check if order updated or not
                    if (orderUpdated != null)
                    {
                        //Create Fulfillment
                        var fulfillment = new Fulfillment()
                        {
                            LocationId = ConfigHelper.LocationID,
                            NotifyCustomer = true//need to add more things
                        };
                        fulfillment = await fulfillmentService.CreateAsync(order.Id ?? 0, fulfillment);

                        //Close Order
                        //await orderService.CloseAsync(order.Id ?? 0);
                    }
                    else
                    {
                        _logger.Info(ToStringHttpCustomResponse(OrderHttpCustomResponse.KeyRetrievalError));
                        return ToStringHttpCustomResponse(OrderHttpCustomResponse.KeyRetrievalError);
                    }
                }
                else
                {
                    _logger.Info(ToStringHttpCustomResponse(OrderHttpCustomResponse.KeyRetrievalError));
                    return ToStringHttpCustomResponse(OrderHttpCustomResponse.KeyRetrievalError);
                }
                //Everything went good
                return HttpStatusCode.OK.ToString();
            }
            catch (Exception exp)
            {
                _logger.Error("CompleteOrder", exp);
                return HttpStatusCode.InternalServerError.ToString();
            }
        }
    }
}