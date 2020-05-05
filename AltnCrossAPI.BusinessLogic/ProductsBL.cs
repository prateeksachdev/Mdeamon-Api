using altn_common.Catalog;
using altn_common.KeyCodes;
using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Shared;
using System;
using System.Net;
using static AltnCrossAPI.Shared.Enums;

namespace AltnCrossAPI.BusinessLogic
{
    public class ProductsBL: IProductsBL
    {
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
        public Result GetUnitPrice(string productCode = "",
            string skuString = "", SkuType skuType = SkuType.NEW, int newQty = 1, string oldKey = "",
            int duration = 1, string userId = "", string userEmail = "")
        {
            Result result = new Result();
            try
            {
                if (productCode == "" && skuString == "")
                {
                    result.Message = ToStringHttpCustomResponse(ProductsHttpCustomResponse.UnitPriceCodeOrSkuError);
                    return result;
                }

                ProductSku sku;
                if (skuString != "")
                    sku = new ProductSku(skuString);
                else
                    sku = new ProductSku(productCode, string.Empty, skuType, ProductType.PRO, ProductTierSize.OneUser, ProductType.PRO, ProductTierSize.None, duration, ProductDurationType.YR);

                RegKey oldRegKey;
                if (!string.IsNullOrEmpty(oldKey))
                    oldRegKey = new RegKey(oldKey);
                else
                    oldRegKey = null;

                //Get unit price form database using client's dll
                UnitPriceResponse response = Catalog.GetUnitPrice(sku, newQty, oldRegKey, int.MinValue, DateTime.MinValue, userId, userEmail);
                result.Data = new
                {
                    response.SAPrice,
                    response.DiscountPrice,
                    response.ListPrice,
                    response.NewQty,
                    response.OldQty,
                    response.SAParentPrice,
                    response.Description,
                    response.SKU
                };
                result.Status = true;
                result.Message = HttpStatusCode.OK.ToString();
            }
            catch (CatalogException cExp)
            {
                result.Message = cExp.Message;
            }
            catch (Exception exp)
            {
                result.Message = exp.Message;
            }
            return result;
        }
    }
}