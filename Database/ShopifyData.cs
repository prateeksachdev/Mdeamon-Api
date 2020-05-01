using AltnCrossAPI.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyData : DBHelper, IShopifyData
    {
        /// <summary>
        /// Method to store Shopify JSON Data
        /// </summary>
        /// <returns>Id of the record inserted</returns>
        public int ShopifyDataInsert(ShopifyDataModel model)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyDataInsert";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@JSON", model.JSON));
                cmdToExecute.Parameters.Add(new SqlParameter("@EventType", model.EventType));
                cmdToExecute.Parameters.Add(new SqlParameter("@DateAdded", model.DateAdded));
                cmdToExecute.Parameters.Add(new SqlParameter("@Entity", model.Entity));
                cmdToExecute.Parameters.Add(new SqlParameter("@Id", model.Id));
                cmdToExecute.Parameters["@Id"].Direction = ParameterDirection.Output;

                OpenConnection();

                cmdToExecute.ExecuteReader();
                return Convert.ToInt32(cmdToExecute.Parameters["@Id"].Value);
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

        /// <summary>
        /// Method to update event type of Shopify JSON Data
        /// </summary>
        public void ShopifyDataUpdate(int id)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "ShopifyDataUpdate";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ShopifyDataId", id));

                OpenConnection();

                cmdToExecute.ExecuteReader();
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