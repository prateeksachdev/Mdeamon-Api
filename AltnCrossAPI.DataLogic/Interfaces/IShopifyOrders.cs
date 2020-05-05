using AltnCrossAPI.Database.Models;
using System.Data;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IShopifyOrders
    {
        DataTable ShopifyOrderByShopifyIdGet(long shopifyId);
        void ShopifyOrderInsertUpdate(ShopifyOrderModel model);
    }
}