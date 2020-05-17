using altn_common.KeyCodes;
using AltnCrossAPI.Database.ViewModels;
using AltnCrossAPI.Shared;

namespace AltnCrossAPI.BusinessLogic.Interfaces
{
    public interface IProductsBL
    {
        Result GetUnitPrice(string productCode = "",
            string skuString = "", SkuType skuType = SkuType.NEW, int newQty = 1, string oldKey = "",
            int duration = 1, string userId = "", string userEmail = "");

        KeyValidityViewModel GetKeyValidity(string versionString, string key);
    }
}