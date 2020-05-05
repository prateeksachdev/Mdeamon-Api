using AltnCrossAPI.Database.Models;
using System.Data;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IShopifyOrderAddresses
    {
        DataTable ShopifyOrderAddressesByOrderIdGet(long orderId);

        void ShopifyOrderAddressInsertUpdate(ShopifyOrderAddressModel model);

        void ShopifyOrderAddresssesDelete(string whereClause);
    }
}