using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyOrderLineItems : IShopifyOrderLineItems
    {
        private readonly DBHelper _dbHelper;
        public ShopifyOrderLineItems()
        {
            _dbHelper = new DBHelper();
        }
        public DataTable ShopifyOrderLineItemsByOrderIdGet(long orderId)
        {
            SqlParameter[] parameters = { new SqlParameter("@OrderId", orderId) };
            return _dbHelper.ExecuteProcedure("ShopifyOrderLineItemsByOrderIdGet", parameters);
        }

        public void ShopifyOrderLineItemInsertUpdate(ShopifyOrderLineItemModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", model.ShopifyId),
            new SqlParameter("@OrderId", model.OrderId),
            new SqlParameter("@VariantId", model.VariantId),
            new SqlParameter("@Title", model.Title),
            new SqlParameter("@Quantity", model.Quantity),
            new SqlParameter("@FulfillableQuantity", model.FulfillableQuantity),
            new SqlParameter("@SKU", model.SKU),
            new SqlParameter("@Vendor", model.Vendor),
            new SqlParameter("@ProductId", model.ProductId),
            new SqlParameter("@Price", model.Price),
            new SqlParameter("@TotalDiscount", model.TotalDiscount),
            new SqlParameter("@TaxCode", model.TaxCode)
            };

            _dbHelper.ExecuteNonQuery("ShopifyOrderLineItemInsertUpdate", parameters);
        }

        public void ShopifyOrderLineItemRegKeyUpdate(long orderId, long shopifyId, string regKey)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", shopifyId),
            new SqlParameter("@OrderId", orderId),
            new SqlParameter("@RegKey", regKey)
            };

            _dbHelper.ExecuteNonQuery("ShopifyOrderLineItemRegKeyUpdate", parameters);
        }

        public void ShopifyOrderLineItemsDelete(string whereClause)
        {
            SqlParameter[] parameters = { new SqlParameter("@WhereClause", whereClause) };
            _dbHelper.ExecuteNonQuery("ShopifyOrderLineItemsDelete", parameters);
        }
    }
}