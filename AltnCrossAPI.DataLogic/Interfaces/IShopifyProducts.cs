using AltnCrossAPI.Database.Models;
using System.Data;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IShopifyProducts
    {
        void ShopifyProductDelete(long shopifyProductId);
        void ShopifyProductVariantCountUpdate(long shopifyId, int count);
        void ShopifyProductInsertUpdate(ShopifyProductModel model);
        ShopifyProductModel ShopifyProductGet(long? productId);
        ShopifyProductModel ShopifyProductByParentProductGetLast(long? parentProductId);
    }
}