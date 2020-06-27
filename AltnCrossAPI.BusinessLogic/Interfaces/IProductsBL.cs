using altn_common.KeyCodes;
using AltnCrossAPI.Database.ViewModels;
using AltnCrossAPI.Shared;
using ShopifySharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AltnCrossAPI.BusinessLogic.Interfaces
{
    public interface IProductsBL
    {
        Result GetUnitPrice(string productCode = "",
            string skuString = "", SkuType skuType = SkuType.NEW, int newQty = 1, string oldKey = "",
            int duration = 1, string userId = "", string userEmail = "");

        KeyValidityViewModel GetKeyValidity(string versionString, string key);
        Task<string> ProductSync(Product order);
        Task<Result> CustomVariant(List<CustomVariantViewModel> variantModel);
        Result Delete(Product product);
    }
}