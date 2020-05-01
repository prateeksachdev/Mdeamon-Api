using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public interface IShopifyOrders
    {
        DataTable ShopifyOrderByShopifyIdGet(long shopifyId);
        void ShopifyOrderInsertUpdate(ShopifyOrderModel model);
    }
}