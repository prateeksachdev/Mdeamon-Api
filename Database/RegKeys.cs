using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class RegKeys : DBHelper, IRegKeys
    {
        public string RegKeyStringGet(RegKeyModel model)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "select KeyString from RegKeys where UserID = @UserID and UserEmail = @UserEmail and SKU = @SKU and ProductSize = @ProductSize";
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.CommandTimeout = 0;
            cmdToExecute.Connection = DBConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductSize", SqlDbType.SmallInt, 5, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.ProductSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.UserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserEmail", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.Username));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, model.SKU));

                OpenConnection();

                // Execute query.
                var dr = cmdToExecute.ExecuteReader();
                return dr.Read() ? dr.GetString(0) : "";
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