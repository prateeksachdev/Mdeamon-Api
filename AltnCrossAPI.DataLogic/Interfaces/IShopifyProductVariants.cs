using AltnCrossAPI.Database.Models;
using System.Data;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IShopifyProductVariants
    {
        DataTable ShopifyProductVariantsByProductIdGet(long productId);

        void ShopifyProductVariantInsertUpdate(ShopifyProductVariantModel model);

        long ShopifyProductVariantIdGet(long productId, decimal price);

        void ShopifyProductVariantsDelete(string whereClause, long productId);
    }
}