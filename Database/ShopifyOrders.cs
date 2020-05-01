using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyOrder : DBHelper, IShopifyOrders
    {
        public DataTable ShopifyOrderByShopifyIdGet(long shopifyId)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderByShopifyIdGet";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ShopifyId", shopifyId));

                OpenConnection();

                DataTable tableToReturn = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmdToExecute);
                adapter.Fill(tableToReturn);
                return tableToReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(false);
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public void ShopifyOrderInsertUpdate(ShopifyOrderModel model)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderInsertUpdate";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ShopifyId", model.ShopifyId));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderNumber", model.OrderNumber));
                cmdToExecute.Parameters.Add(new SqlParameter("@ShopifyDataId", model.ShopifyDataId));
                cmdToExecute.Parameters.Add(new SqlParameter("@Email", model.Email));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedOn", model.CreatedOn));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedOn", model.UpdatedOn));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProcessedOn", model.ProcessedOn));
                cmdToExecute.Parameters.Add(new SqlParameter("@Token", model.Token));
                cmdToExecute.Parameters.Add(new SqlParameter("@CheckoutToken", model.CheckoutToken));
                cmdToExecute.Parameters.Add(new SqlParameter("@Gateway", model.Gateway));
                cmdToExecute.Parameters.Add(new SqlParameter("@TotalPrice", model.TotalPrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@TotalDiscount", model.TotalDiscount));
                cmdToExecute.Parameters.Add(new SqlParameter("@SubTotalPrice", model.SubTotalPrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@TotalTax", model.TotalTax));
                cmdToExecute.Parameters.Add(new SqlParameter("@FinancialStatus", model.FinancialStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProcessingMethod", model.ProcessingMethod));
                cmdToExecute.Parameters.Add(new SqlParameter("@Currency", model.Currency));
                cmdToExecute.Parameters.Add(new SqlParameter("@CheckoutId", model.CheckoutId));
                cmdToExecute.Parameters.Add(new SqlParameter("@AppId", model.AppId));
                cmdToExecute.Parameters.Add(new SqlParameter("@BrowserIP", model.BrowserIP));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderStatusUrl", model.OrderStatusUrl));

                OpenConnection();

                cmdToExecute.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(false);
                cmdToExecute.Dispose();
            }
        }
    }
}