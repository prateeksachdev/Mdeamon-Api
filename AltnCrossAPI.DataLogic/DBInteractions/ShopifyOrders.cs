using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyOrder : IShopifyOrders
    {
        private readonly DBHelper _dbHelper;
        public ShopifyOrder()
        {
            _dbHelper = new DBHelper();
        }
        public DataTable ShopifyOrderByShopifyIdGet(long shopifyId)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", shopifyId) };
            return _dbHelper.ExecuteProcedure("ShopifyOrderByShopifyIdGet", parameters);
        }

        public void ShopifyOrderInsertUpdate(ShopifyOrderModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", model.ShopifyId),
            new SqlParameter("@OrderNumber", model.OrderNumber),
            new SqlParameter("@ShopifyDataId", model.ShopifyDataId),
            new SqlParameter("@Email", model.Email),
            new SqlParameter("@CreatedOn", model.CreatedOn),
            new SqlParameter("@UpdatedOn", model.UpdatedOn),
            new SqlParameter("@ProcessedOn", model.ProcessedOn),
            new SqlParameter("@Token", model.Token),
            new SqlParameter("@CheckoutToken", model.CheckoutToken),
            new SqlParameter("@Gateway", model.Gateway),
            new SqlParameter("@TotalPrice", model.TotalPrice),
            new SqlParameter("@TotalDiscount", model.TotalDiscount),
            new SqlParameter("@SubTotalPrice", model.SubTotalPrice),
            new SqlParameter("@TotalTax", model.TotalTax),
            new SqlParameter("@FinancialStatus", model.FinancialStatus),
            new SqlParameter("@ProcessingMethod", model.ProcessingMethod),
            new SqlParameter("@Currency", model.Currency),
            new SqlParameter("@CheckoutId", model.CheckoutId),
            new SqlParameter("@AppId", model.AppId),
            new SqlParameter("@BrowserIP", model.BrowserIP),
            new SqlParameter("@OrderStatusUrl", model.OrderStatusUrl)
            };

            _dbHelper.ExecuteNonQuery("ShopifyOrderInsertUpdate", parameters);
        }
    }
}