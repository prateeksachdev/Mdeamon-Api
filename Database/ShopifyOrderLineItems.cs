using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyOrderLineItems : DBHelper, IShopifyOrderLineItems
    {
        public DataTable ShopifyOrderLineItemsByOrderIdGet(long orderId)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderLineItemsByOrderIdGet";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderId", orderId));

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

        public void ShopifyOrderLineItemInsertUpdate(ShopifyOrderLineItemModel model)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderLineItemInsertUpdate";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ShopifyId", model.ShopifyId));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderId", model.OrderId));
                cmdToExecute.Parameters.Add(new SqlParameter("@VariantId", model.VariantId));
                cmdToExecute.Parameters.Add(new SqlParameter("@Title", model.Title));
                cmdToExecute.Parameters.Add(new SqlParameter("@Quantity", model.Quantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@FulfillableQuantity", model.FulfillableQuantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKU", model.SKU));
                cmdToExecute.Parameters.Add(new SqlParameter("@Vendor", model.Vendor));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductId", model.ProductId));
                cmdToExecute.Parameters.Add(new SqlParameter("@Price", model.Price));
                cmdToExecute.Parameters.Add(new SqlParameter("@TotalDiscount", model.TotalDiscount));
                cmdToExecute.Parameters.Add(new SqlParameter("@TaxCode", model.TaxCode));

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

        public void ShopifyOrderLineItemsDelete(string whereClause)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderLineItemsDelete";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@WhereClause", whereClause));

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