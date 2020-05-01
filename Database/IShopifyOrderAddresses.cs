using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public interface IShopifyOrderAddresses
    {
        DataTable ShopifyOrderAddressesByOrderIdGet(long orderId);

        void ShopifyOrderAddressInsertUpdate(ShopifyOrderAddressModel model);

        void ShopifyOrderAddresssesDelete(string whereClause);
    }
}