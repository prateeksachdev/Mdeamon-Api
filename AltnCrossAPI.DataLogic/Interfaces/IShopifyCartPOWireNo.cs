using AltnCrossAPI.Database.Models;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IShopifyCartPOWireNo
    {
        void ShopifyCartPOWireNoInsertUpdate(ShopifyCartPOWireNoModel model);

        string ShopifyCartPOWireNoGet(string cartToken);
    }
}