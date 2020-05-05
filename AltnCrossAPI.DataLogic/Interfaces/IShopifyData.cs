using AltnCrossAPI.Database.Models;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IShopifyData
    {
        int ShopifyDataInsert(ShopifyDataModel model);

        void ShopifyDataUpdate(int id);
    }
}