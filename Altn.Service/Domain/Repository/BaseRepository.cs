using Dapper;
using System.Data.SqlClient;

namespace Altn.Service.Domain.Repository
{
    public class BaseRepository
    {
        public void ExecuteQuery(string query, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
            {
                connection.Open();
                connection.Query(query, parameters);
            }
        }
    }
}