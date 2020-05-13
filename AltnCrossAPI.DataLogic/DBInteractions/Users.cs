using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class Users : IUsers
    {
        private readonly DBHelper _dbHelper;
        public Users()
        {
            _dbHelper = new DBHelper();
        }
        /// <summary>
        /// Method to Add Shopify Customer Id to User
        /// </summary>
        public void UserShopifyCustomerIDUpdate(UsersModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserID", model.UserID),
            new SqlParameter("@ShopifyCustomerID", model.ShopifyCustomerID)
            };

            _dbHelper.ExecuteNonQuery("UserShopifyCustomerIDUpdate", parameters);
        }

        /// <summary>
        /// Method to Delete User based on Shopify Customer ID
        /// </summary>
        public void UserDeleteByShopifyCustomerID(long? shopifyCustomerId)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyCustomerID", shopifyCustomerId) };
            _dbHelper.ExecuteNonQuery("UserDeleteByShopifyCustomerID", parameters);
        }

        /// <summary>
        /// Method to Get User based on Shopify Customer ID
        /// </summary>
        public UsersModel UserGetByShopifyCustomerID(long? shopifyCustomerId)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyCustomerID", shopifyCustomerId) };
            var data = _dbHelper.ExecuteProcedure("UserGetByShopifyCustomerID", parameters);
            if (data.Rows.Count > 0)
            {
                UsersModel model = new UsersModel
                {
                    UserID = data.Rows[0]["UserID"].ToString(),
                    UserEmail = data.Rows[0]["UserEmail"].ToString(),
                    ShopifyCustomerID= long.Parse(data.Rows[0]["ShopifyCustomerID"].ToString()),
                    UserType = int.Parse(data.Rows[0]["UserType"].ToString()),
                    IsPopulated = true
                };
                return model;
            }
            else
            {
                return new UsersModel();
            }
        }
    }
}