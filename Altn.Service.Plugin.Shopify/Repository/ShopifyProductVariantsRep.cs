using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Altn.Service.Plugin.Shopify.Dto;

namespace Altn.Service.Plugin.Shopify.DataAccess
{
    internal static class ShopifyProductVariantsRep
    {
        internal static IEnumerable<ShopifyProductVariantsDto> Get(string connectionString, int NumberOfDaysOld = 0)
        {
            var query = "SELECT * FROM [dbo].[ShopifyProductVariants] WHERE Title like '%CustomVariant%' and CreatedOn < @Date";
            var param = new DynamicParameters();

            param.Add("Date", DateTime.Now.AddDays(NumberOfDaysOld * -1));

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<ShopifyProductVariantsDto>(query, param, commandType: CommandType.Text);
            }
        }
    }
}