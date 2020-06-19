using AltnCrossAPI.Database.Models;
using System.Data;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IShopifyOrderLineItems
    {
        DataTable ShopifyOrderLineItemsByOrderIdGet(long orderId);

        void ShopifyOrderLineItemInsertUpdate(ShopifyOrderLineItemModel model);

        void ShopifyOrderLineItemRegKeyUpdate(long orderId, long shopifyId, string regKey);

        void ShopifyOrderLineItemsDelete(string whereClause);
    }
}