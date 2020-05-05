using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class RegKeys : IRegKeys
    {
        private readonly DBHelper _dbHelper;
        public RegKeys()
        {
            _dbHelper = new DBHelper();
        }
        public string RegKeyStringGet(RegKeyModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@ProductSize", SqlDbType.SmallInt, 5, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.ProductSize),
            new SqlParameter("@UserID", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.UserID),
            new SqlParameter("@UserEmail", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.Username),
            new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.SKU)
            };

            return _dbHelper.ExecuteReaderQuery("select KeyString from RegKeys where UserID = @UserID and UserEmail = @UserEmail and SKU = @SKU and ProductSize = @ProductSize", parameters);            
        }
    }
}