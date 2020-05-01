using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyOrderAddresses : DBHelper, IShopifyOrderAddresses
    {
        public DataTable ShopifyOrderAddressesByOrderIdGet(long orderId)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderAddressesByOrderIdGet";
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

        public void ShopifyOrderAddressInsertUpdate(ShopifyOrderAddressModel model)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderAddressInsertUpdate";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@FirstName", model.FirstName));
                cmdToExecute.Parameters.Add(new SqlParameter("@LastName", model.LastName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Address", model.Address));
                cmdToExecute.Parameters.Add(new SqlParameter("@Phone", model.Phone));
                cmdToExecute.Parameters.Add(new SqlParameter("@City", model.City));
                cmdToExecute.Parameters.Add(new SqlParameter("@Zip", model.Zip));
                cmdToExecute.Parameters.Add(new SqlParameter("@Province", model.Province));
                cmdToExecute.Parameters.Add(new SqlParameter("@Country", model.Country));
                cmdToExecute.Parameters.Add(new SqlParameter("@Latitude", model.Latitude));
                cmdToExecute.Parameters.Add(new SqlParameter("@Longitude", model.Longitude));
                cmdToExecute.Parameters.Add(new SqlParameter("@CountryCode", model.CountryCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderId", model.OrderId));
                cmdToExecute.Parameters.Add(new SqlParameter("@AddressType", model.AddressType));

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

        public void ShopifyOrderAddresssesDelete(string whereClause)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyOrderAddresssesDelete";
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