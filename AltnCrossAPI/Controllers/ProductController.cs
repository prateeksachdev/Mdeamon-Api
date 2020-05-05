using altn_common.KeyCodes;
using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Database.ViewModels;
using AltnCrossAPI.Shared;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace AltnCrossAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductsBL _productsBL;

        public ProductController(IProductsBL productsBL)
        {
            _productsBL = productsBL;
        }

        /// <summary>
        /// Checks if key is valid for given version of the product
        /// </summary>
        /// <param name="versionString">Product version</param>
        /// <param name="key">Registration key to be checked</param>
        /// <returns>Returns boolean value based on key validity</returns>
        [HttpGet]
        public JsonResult<KeyValidityViewModel> GetKeyValidity(string versionString = "", string key = "")
        {
            try
            {
                RegKey regKey = new RegKey(key);

                ProductVersion version = new ProductVersion(versionString);

                return Json(new KeyValidityViewModel { ErrorMessage = HttpStatusCode.OK.ToString(), isValid = regKey.IsValidForVersion(version) });
            }
            catch (Exception exp)
            {
                return Json(new KeyValidityViewModel { ErrorMessage = exp.Message });
            }
        }

        /// <summary>
        /// Gets the unit price based on productCode or skuString.
        /// </summary>
        /// <param name="productCode">Product code</param>
        /// <param name="skuString"></param>
        /// <param name="skuType"></param>
        /// <param name="newQty"></param>
        /// <param name="oldKey"></param>
        /// <param name="duration"></param>
        /// <param name="userId">User id against which key needs to be checked</param>
        /// <param name="userEmail">User email against which key needs to be checked</param>
        /// <returns>Returns unit price of the product</returns>
        [HttpGet]
        public JsonResult<UnitPriceResponseViewModel> GetUnitPrice(string productCode = "",
            string skuString = "", SkuType skuType = SkuType.NEW, int newQty = 1, string oldKey = "",
            int duration = 1, string userId = "", string userEmail = "")
        {
            Result result = _productsBL.GetUnitPrice(productCode, skuString, skuType, newQty, oldKey, duration, userId, userEmail);
            return Json(new UnitPriceResponseViewModel { ErrorMessage = result.Message, priceResponse = result.Data });
        }
    }
}
