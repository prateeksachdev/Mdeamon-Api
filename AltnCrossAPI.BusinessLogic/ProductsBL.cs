﻿using altn_common.Catalog;
using altn_common.KeyCodes;
using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Database.ViewModels;
using AltnCrossAPI.Shared;
using System;
using System.Net;
using static AltnCrossAPI.Shared.Enums;

namespace AltnCrossAPI.BusinessLogic
{
    public class ProductsBL : IProductsBL
    {
        /// <summary>
        /// Checks if key is valid for given version of the product
        /// </summary>
        /// <param name="versionString">Product version</param>
        /// <param name="key">Registration key to be checked</param>
        /// <returns>Returns result based on key validity</returns>
        public KeyValidityViewModel GetKeyValidity(string versionString = "", string key = "")
        {
            try
            {
                RegKey regKey = new RegKey(key);

                ProductVersion version = new ProductVersion(versionString);
                if (regKey.IsValidForVersion(version))
                {
                    return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.OK.ToString(), AdditionalInfo = string.Format("Key expires on {0}", regKey.EndDate.ToString("yyyy/MM/dd")), isValid = true };
                }
                else if (regKey.IsPopulated)
                {
                    return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.OK.ToString(), AdditionalInfo = string.Format("The key you have entered is for another product({0})", regKey.ProductCode), isValid = false };
                }
                else
                {
                    return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.OK.ToString(), AdditionalInfo = "The key you have entered is not valid", isValid = false };
                }
            }
            catch
            {
                return new KeyValidityViewModel { ErrorMessage = HttpStatusCode.InternalServerError.ToString() };
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
        public Result GetUnitPrice(string productCode = "",
            string skuString = "", SkuType skuType = SkuType.NEW, int newQty = 1, string oldKey = "",
            int duration = 1, string userId = "", string userEmail = "")
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrWhiteSpace(productCode) && string.IsNullOrWhiteSpace(skuString))
                {
                    result.Message = ToStringHttpCustomResponse(ProductsHttpCustomResponse.UnitPriceCodeOrSkuError);
                    return result;
                }

                ProductSku sku;
                if (!string.IsNullOrWhiteSpace(skuString))
                    sku = new ProductSku(skuString);
                else
                    sku = new ProductSku(productCode, string.Empty, skuType, ProductType.PRO, ProductTierSize.OneUser, ProductType.PRO, ProductTierSize.None, duration, ProductDurationType.YR);

                UnitPriceResponse response;
                if (!string.IsNullOrEmpty(oldKey))
                {
                    RegKey oldKeyObj = new RegKey(oldKey);
                    response = Catalog.GetUnitPrice(sku, (oldKeyObj.ProductSize > 0 ? oldKeyObj.ProductSize : 5), oldKeyObj, int.MinValue, DateTime.MinValue, userId, userEmail);
                }
                else
                    response = Catalog.GetUnitPrice(sku, newQty, null, int.MinValue, DateTime.MinValue, userId, userEmail);

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