using altn_common.Catalog;
using altn_common.KeyCodes;
using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using AltnCrossAPI.Database.ViewModels;
using AltnCrossAPI.Shared;
using AltnCrossAPI.Shared.Logging;
using Newtonsoft.Json;
using ShopifySharp;
using ShopifySharp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using static AltnCrossAPI.Shared.Enums;

namespace AltnCrossAPI.BusinessLogic
{
    public class ProductsBL : IProductsBL
    {
        private readonly ProductVariantService variantService = new ProductVariantService(ConfigHelper.ShopifyUrl, ConfigHelper.ShopAccessToken);
        private readonly InventoryItemService inventoryItemService = new InventoryItemService(ConfigHelper.ShopifyUrl, ConfigHelper.ShopAccessToken);
        private readonly MetaFieldService metaFieldService = new MetaFieldService(ConfigHelper.ShopifyUrl, ConfigHelper.ShopAccessToken);
        private readonly ProductService productService = new ProductService(ConfigHelper.ShopifyUrl, ConfigHelper.ShopAccessToken); private IShopifyData _shopifyData;
        private ILogger _logger;
        private IShopifyProducts _product;
        private IShopifyProductVariants _productVariant;

        public ProductsBL(IShopifyProducts product,
                          IShopifyProductVariants productVariant,
                          IShopifyData shopifyData,
                          ILogger logger)
        {
            _shopifyData = shopifyData;
            _logger = logger;
            _product = product;
            _productVariant = productVariant;
        }
        /// <summary>
        /// Checks if key is valid for given version of the product
        /// </summary>
        /// <param name="versionString">Product version</param>
        /// <param name="key">Registration key to be checked</param>
        /// <returns>Returns result based on key validity</returns>
        public KeyValidityViewModel GetKeyValidity(string versionString = "", string key = "")
        {
            try
            {
                RegKey regKey = new RegKey(key);

                ProductVersion version = new ProductVersion(versionString);
                if (regKey.IsValidForVersion(version))
                {
                    return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.OK.ToString(), AdditionalInfo = string.Format("Key expires on {0}", regKey.EndDate.ToString("yyyy/MM/dd")), isValid = true };
                }
                else if (regKey.IsPopulated)
                {
                    return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.OK.ToString(), AdditionalInfo = string.Format("The key you have entered is for another product({0})", regKey.ProductCode), isValid = false };
                }
                else
                {
                    return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.OK.ToString(), AdditionalInfo = "The key you have entered is not valid", isValid = false };
                }
            }
            catch
            {
                return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.InternalServerError.ToString() };
            }
        }


        /// <summary>
        /// Gets the unit price based on productCode or skuString.
        /// </summary>
        /// <param name="productCode">Product code</param>
        /// <param name="skuString"></param>
        /// <param name="skuType"></param>
        /// <param name="newQty"></param>
        /// <param name="oldKey"></param>
        /// <param name="duration"></param>
        /// <param name="userId">User id against which key needs to be checked</param>
        /// <param name="userEmail">User email against which key needs to be checked</param>
        /// <returns>Returns unit price of the product</returns>
        public Result GetUnitPrice(string productCode = "",
            string skuString = "", SkuType skuType = SkuType.NEW, int newQty = 1, string oldKey = "",
            int duration = 1, string userId = "", string userEmail = "")
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrWhiteSpace(productCode) && string.IsNullOrWhiteSpace(skuString))
                {
                    result.Message = ToStringHttpCustomResponse(ProductsHttpCustomResponse.UnitPriceCodeOrSkuError);
                    return result;
                }

                ProductSku sku;
                if (!string.IsNullOrWhiteSpace(skuString))
                    sku = new ProductSku(skuString);
                else
                    sku = new ProductSku(productCode, string.Empty, skuType, ProductType.PRO, ProductTierSize.OneUser, ProductType.PRO, ProductTierSize.None, duration, ProductDurationType.YR);

                UnitPriceResponse response;
                if (!string.IsNullOrEmpty(oldKey))
                {
                    RegKey oldKeyObj = new RegKey(oldKey);
                    response = Catalog.GetUnitPrice(sku, (oldKeyObj.ProductSize > 0 ? oldKeyObj.ProductSize : 5), oldKeyObj, int.MinValue, DateTime.MinValue, userId, userEmail);
                }
                else
                    response = Catalog.GetUnitPrice(sku, newQty, null, int.MinValue, DateTime.MinValue, userId, userEmail);

                result.Data = new
                {
                    response.SAPrice,
                    response.DiscountPrice,
                    response.ListPrice,
                    response.NewQty,
                    response.OldQty,
                    response.SAParentPrice,
                    response.Description,
                    response.SKU
                };
                result.Status = true;
                result.Message = HttpStatusCode.OK.ToString();
            }
            catch (CatalogException cExp)
            {
                result.Message = cExp.Message;
            }
            catch (Exception exp)
            {
                result.Message = exp.Message;
            }
            return result;
        }

        /// <summary>
        /// Returns VariantIds object based on the productid and price array passed. 
        /// It creates new Variant if it does not exist already.
        /// </summary>
        /// <param name="variantModel">List of ProductId and Price</param>
        /// <returns>returns VariantIds object based on the productid and price array passed.</returns>
        public async Task<Result> CustomVariant(List<CustomVariantViewModel> variantModel)
        {
            Result result = new Result() { Message = HttpStatusCode.OK.ToString() };
            try
            {
                foreach (CustomVariantViewModel model in variantModel)
                {
                    long variantId = _productVariant.ShopifyProductVariantIdGet(model.ProductId, model.Price);
                    if (variantId > 0)//check if product already has this variant
                    {
                        model.VariantId = variantId;
                    }
                    else
                    {
                        var localProduct = _product.ShopifyProductByParentProductGetLast(model.ProductId);//check if it already exists as other Product's parent
                        Product product;
                        if (localProduct?.ShopifyId > 0)
                        {
                            variantId = _productVariant.ShopifyProductVariantIdGet(localProduct.ShopifyId, model.Price);
                            model.ProductId = localProduct.ShopifyId;
                            if (variantId > 0)//check if child product already has this variant
                            {
                                model.VariantId = variantId;
                                continue;
                            }
                            else
                            {
                                product = await productService.GetAsync(localProduct.ShopifyId);//continue with child product
                            }
                        }
                        else
                            product = await productService.GetAsync(model.ProductId);//child does not exists. continue with current product

                        var defaultVariant = product.Variants.OrderBy(v => v.CreatedAt).FirstOrDefault();//To get SKU for new variant
                        //Create Custom Variant
                        ProductVariant variant = new ProductVariant()
                        {
                            ProductId = product.Id,
                            Price = model.Price,
                            SKU = defaultVariant.SKU,
                            Barcode = "custom",
                            RequiresShipping = false
                        };

                        for (int index = 0; index < product.Options.Count(); index++)
                        {
                            switch (index)
                            {
                                case 0:
                                    variant.Option1 = defaultVariant.SKU + (model.Quantity > 1 ? ("_" + model.Quantity + "YR") : "") + "_" + model.Price + "_" + model.UserCount;//Variant Title depeds upon Options provided and Shopify does not create same title again and again
                                    break;
                                case 1:
                                    variant.Option2 = defaultVariant.SKU;
                                    break;
                                case 3:
                                    variant.Option3 = "Custom";
                                    break;
                            }
                        }


                        if (product.Variants.Count() > 99)//Create duplicate product in shopify becasue shopify allow 100 variants max
                        {
                            //var existingField = await metaFieldService.ListAsync(5084926017676, "products");
                            Product newProduct = new Product()
                            {
                                Title = product.Title,
                                Vendor = product.Vendor,
                                BodyHtml = product.BodyHtml,
                                ProductType = product.ProductType,
                                Metafields = (new MetaField[] { new MetaField { Namespace = "product_duplicate", Key = "ParentProductHandle", Value = product.Handle, ValueType = "string" }, new MetaField { Namespace = "product_duplicate", Key = "ParentProductID", Value = product.Id.ToString(), ValueType = "string" } }),
                                Options = product.Options,
                                Images = product.Images,
                                Variants = new ProductVariant[] { variant }
                            };
                            newProduct = await productService.CreateAsync(newProduct);

                            model.ProductId = newProduct.Id ?? 0;
                            variant = newProduct.Variants.ToList().First();
                            goto SetTracking;
                        }

                        variant = await variantService.CreateAsync(variant.ProductId ?? 0, variant);

                    SetTracking:
                        model.VariantId = variant.Id ?? 0;

                        //Variant can be updated only via level
                        var ids = new List<long>()
                        {
                            variant.InventoryItemId.Value
                        };

                        var inventoryItemFilter = new InventoryItemListFilter()
                        {
                            Ids = ids
                        };

                        var items = await inventoryItemService.ListAsync(inventoryItemFilter);

                        var item = items.Items.First();

                        item.Tracked = false;

                        await inventoryItemService.UpdateAsync(item.Id ?? 0, item);
                    }
                }
                result.Data = variantModel;
            }
            catch (ShopifyException ex)
            {
                _logger.Error("CustomVariant", ex);
                result.Message = ex.Message;
            }
            catch (Exception exp)
            {
                _logger.Error("CustomVariant", exp);
                result.Message = HttpStatusCode.InternalServerError.ToString();
            }
            return result;
        }


        /// <summary>
        /// Inserts a new shopify product or Update existing shopify product in db
        /// </summary>
        /// <param name="product">ShopifySharp Product posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        public async Task<string> ProductSync(Product product)
        {
            try
            {
                //Adding logs of the json posted from shopify to the database 
                ShopifyDataModel sData = new ShopifyDataModel();
                sData.JSON = JsonConvert.SerializeObject(product);
                sData.EventType = "Insert";//By default insert. Updated via ShopifyProduct Insert/Updated
                sData.Entity = "Product";
                sData.DateAdded = DateTime.Now;
                int shopifyDataId = _shopifyData.ShopifyDataInsert(sData);

                //check meta fields for parent product if any
                long parentProductId = 0;
                var metaFields = await metaFieldService.ListAsync(product.Id ?? 0, "products");
                if (metaFields.Items.Any() && metaFields.Items.Any(m => m.Key == "ParentProductID"))
                    long.TryParse((metaFields).Items.FirstOrDefault(m => m.Key == "ParentProductID").Value.ToString(), out parentProductId);

                //Insert or Update order in database as per the json received from shopify
                ShopifyProductModel sProduct = new ShopifyProductModel();
                sProduct.ShopifyId = product.Id ?? 0;
                sProduct.Title = product.Title;
                sProduct.ShopifyDataId = shopifyDataId;
                sProduct.Tags = product.Tags;
                sProduct.ProductType = product.ProductType;
                sProduct.Handle = product.Handle;
                sProduct.BodyHtml = product.BodyHtml;
                sProduct.ParentShopifyId = parentProductId;
                _product.ShopifyProductInsertUpdate(sProduct);


                long[] shopifyIds = new long[product.Variants.Count()];
                int index = 0;
                //Insert or Update product variants in database
                foreach (ProductVariant variant in product.Variants)
                {
                    ShopifyProductVariantModel sProductVariant = new ShopifyProductVariantModel();
                    sProductVariant.ShopifyId = variant.Id ?? 0;
                    sProductVariant.ProductId = product.Id ?? 0;
                    sProductVariant.CompareAtPrice = variant.CompareAtPrice;
                    sProductVariant.Price = variant.Price;
                    sProductVariant.Barcode = variant.Barcode;
                    sProductVariant.SKU = variant.SKU;
                    sProductVariant.Title = variant.Title;
                    sProductVariant.Weight = variant.Weight;
                    sProductVariant.WeightUnit = variant.WeightUnit;
                    sProductVariant.InventoryQuantity = variant.InventoryQuantity;
                    sProductVariant.InventoryItemId = variant.InventoryItemId;
                    //sOrderLineItems.TaxCode = item.ta
                    _productVariant.ShopifyProductVariantInsertUpdate(sProductVariant);
                    shopifyIds[index] = variant.Id ?? 0;
                    index++;
                }
                //delete product variants in database which are not part of shopify json now
                if (shopifyIds.Length > 0)
                {
                    string variantsDeleteWhereClause = " WHERE ShopifyId not in (@ShopifyId) and ProductId = " + product.Id;
                    variantsDeleteWhereClause = variantsDeleteWhereClause.Replace("@ShopifyId", string.Join(",", shopifyIds));
                    _productVariant.ShopifyProductVariantsDelete(variantsDeleteWhereClause, product.Id ?? 0);
                }

                return HttpStatusCode.OK.ToString();
            }
            catch (Exception exp)
            {
                _logger.Error("ProductSync", exp);
                return HttpStatusCode.InternalServerError.ToString();
            }
        }

        /// <summary>
        /// Deletes shopify product in db
        /// </summary>
        /// <param name="product">ShopifySharp Product posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        public Result Delete(Product product)
        {
            Result result = new Result();
            try
            {
                //Adding logs of the json posted from shopify to the database
                ShopifyDataModel sData = new ShopifyDataModel();
                sData.JSON = JsonConvert.SerializeObject(product);
                sData.EventType = "Delete";
                sData.Entity = "Product";
                sData.DateAdded = DateTime.Now;
                sData.Id = _shopifyData.ShopifyDataInsert(sData);

                _product.ShopifyProductDelete(product.Id ?? 0);

                result.Message = HttpStatusCode.OK.ToString();
            }
            catch (Exception exp)
            {
                _logger.Error("ProductDelete", exp);
                result.Message = HttpStatusCode.InternalServerError.ToString();
            }
            return result;
        }
    }
}