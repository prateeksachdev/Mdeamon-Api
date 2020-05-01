using AltnCrossAPI.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public interface IShopifyData
    {
        int ShopifyDataInsert(ShopifyDataModel model);

        void ShopifyDataUpdate(int id);
    }
}