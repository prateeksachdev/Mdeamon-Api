using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyCartWireNo : IShopifyCartPOWireNo
    {
        private readonly DBHelper _dbHelper;
        public ShopifyCartWireNo()
        {
            _dbHelper = new DBHelper();
        }
        /// <summary>
        /// Method to store Shopify Cart PO Wire Number
        /// </summary>
        /// <returns></returns>
        public void ShopifyCartPOWireNoInsertUpdate(ShopifyCartPOWireNoModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@CartToken", model.CartToken),
            new SqlParameter("@POWireNo", model.POWireNo)
            };

            _dbHelper.ExecuteNonQuery("ShopifyCartPOWireNoInsertUpdate", parameters);
        }

        /// <summary>
        /// Method to get PO Wire number based on Cart Token
        /// </summary>
        public string ShopifyCartPOWireNoGet(string cartToken)
        {
            SqlParameter[] parameters = { new SqlParameter("@CartToken", cartToken)
            };

            return _dbHelper.ExecuteReaderQuery<string>("select POWireNo from [dbo].[ShopifyCartPOWireNo] where [CartToken] = @CartToken", parameters);
        }
    }
}