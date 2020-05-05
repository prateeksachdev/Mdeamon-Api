using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyData : IShopifyData
    {
        private readonly DBHelper _dbHelper;
        public ShopifyData()
        {
            _dbHelper = new DBHelper();
        }
        /// <summary>
        /// Method to store Shopify JSON Data
        /// </summary>
        /// <returns>Id of the record inserted</returns>
        public int ShopifyDataInsert(ShopifyDataModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@JSON", model.JSON),
            new SqlParameter("@EventType", model.EventType),
            new SqlParameter("@DateAdded", model.DateAdded),
            new SqlParameter("@Entity", model.Entity),
            new SqlParameter("@Id", model.Id)
            };

            return _dbHelper.ExecuteReader("ShopifyDataInsert", parameters, "@Id");
        }

        /// <summary>
        /// Method to update event type of Shopify JSON Data
        /// </summary>
        public void ShopifyDataUpdate(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyDataId", id) };
            _dbHelper.ExecuteNonQuery("ShopifyDataUpdate", parameters);
        }
    }
}