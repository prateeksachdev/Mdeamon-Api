using altn_common.KeyCodes;
using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Database.ViewModels;
using AltnCrossAPI.Shared;
using Newtonsoft.Json;
using ShopifySharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
            return Json(_productsBL.GetKeyValidity(versionString, key));
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

        /// <summary>
        /// Action method to Insert a new shopify product or Update existing shopify product in db
        /// </summary>
        /// <param name="productJson">Product Json posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        [HttpPost]
        [ValidateShopifyRequest]
        [ActionName("ProductSync")]
        public async Task<string> Product(object productJson)
        {
            Product product = JsonConvert.DeserializeObject<Product>(productJson.ToString());
            return await _productsBL.ProductSync(product);
        }


        /// <summary>
        /// Action method to return VariantIds based on the productid and price array passed. 
        /// It creates new Variant if it already does not exist.
        /// </summary>
        /// <param name="productValueArray">Array of Product Ids and corresponding Values</param>
        /// <returns>Returns Variants Ids</returns>
        [HttpPost]
        //[ValidateShopifyRequest]
        [ActionName("CustomVariant")]
        public async Task<JsonResult<ResponseViewModel>> CustomProductVariant(object productValueArray)
        {
            List<CustomVariantViewModel> customVariant = JsonConvert.DeserializeObject<List<CustomVariantViewModel>>(productValueArray.ToString());
            Result result = await _productsBL.CustomVariant(customVariant);
            return Json(new ResponseViewModel { ErrorMessage = result.Message, response = result.Data });
        }

        /// <summary>
        /// Action method to delete product from db
        /// </summary>
        /// <param name="productJson">Product Json posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        [HttpPost]
        [ValidateShopifyRequest]
        [ActionName("Delete")]
        public string DeleteProduct(object productJson)
        {
            Product product = JsonConvert.DeserializeObject<Product>(productJson.ToString());
            return _productsBL.Delete(product).Message;
        }
    }
}
