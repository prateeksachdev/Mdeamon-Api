using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyOrderAddresses : IShopifyOrderAddresses
    {
        private readonly DBHelper _dbHelper;
        public ShopifyOrderAddresses()
        {
            _dbHelper = new DBHelper();
        }
        public DataTable ShopifyOrderAddressesByOrderIdGet(long orderId)
        {
            SqlParameter[] parameters = { new SqlParameter("@OrderId", orderId) };
            return _dbHelper.ExecuteProcedure("ShopifyOrderAddressesByOrderIdGet", parameters);
        }

        public void ShopifyOrderAddressInsertUpdate(ShopifyOrderAddressModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@FirstName", model.FirstName),
            new SqlParameter("@LastName", model.LastName),
            new SqlParameter("@Address", model.Address),
            new SqlParameter("@Phone", model.Phone),
            new SqlParameter("@City", model.City),
            new SqlParameter("@Zip", model.Zip),
            new SqlParameter("@Province", model.Province),
            new SqlParameter("@Country", model.Country),
            new SqlParameter("@Latitude", model.Latitude),
            new SqlParameter("@Longitude", model.Longitude),
            new SqlParameter("@CountryCode", model.CountryCode),
            new SqlParameter("@OrderId", model.OrderId),
            new SqlParameter("@AddressType", model.AddressType)
            };

            _dbHelper.ExecuteNonQuery("ShopifyOrderAddressInsertUpdate", parameters);
        }

        public void ShopifyOrderAddresssesDelete(string whereClause)
        {
            SqlParameter[] parameters = { new SqlParameter("@WhereClause", whereClause) };
            _dbHelper.ExecuteNonQuery("ShopifyOrderAddresssesDelete", parameters);
        }
    }
}