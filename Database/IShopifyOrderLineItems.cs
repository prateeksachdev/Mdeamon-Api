using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public interface IShopifyOrderLineItems
    {
        DataTable ShopifyOrderLineItemsByOrderIdGet(long orderId);

        void ShopifyOrderLineItemInsertUpdate(ShopifyOrderLineItemModel model);

        void ShopifyOrderLineItemsDelete(string whereClause);
    }
}