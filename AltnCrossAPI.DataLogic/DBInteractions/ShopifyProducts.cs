using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyProducts : IShopifyProducts
    {
        private readonly DBHelper _dbHelper;
        public ShopifyProducts()
        {
            _dbHelper = new DBHelper();
        }
        public void ShopifyProductDelete(long shopifyId)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", shopifyId) };
            _dbHelper.ExecuteNonQuery("ShopifyProductDelete", parameters);
        }
        public void ShopifyProductVariantCountUpdate(long shopifyId, int increment)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", shopifyId),
            new SqlParameter("@Increment", increment),
            };
            _dbHelper.ExecuteNonQuery("ShopifyProductVariantCountUpdate", parameters);
        }

        public void ShopifyProductInsertUpdate(ShopifyProductModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", model.ShopifyId),
            new SqlParameter("@ShopifyDataId", model.ShopifyDataId),
            new SqlParameter("@Title", model.Title),
            new SqlParameter("@Tags", model.Tags),
            new SqlParameter("@ProductType", model.ProductType),
            new SqlParameter("@Handle", model.Handle),
            new SqlParameter("@BodyHtml", model.BodyHtml),
            new SqlParameter("@ParentShopifyId", model.ParentShopifyId)
            };

            _dbHelper.ExecuteNonQuery("ShopifyProductInsertUpdate", parameters);
        }

        /// <summary>
        /// Method to Get Product based on Shopify Product ID
        /// </summary>
        /// <param name="productId">Shopify Product Id</param>
        /// <returns>Product Model</returns>
        public ShopifyProductModel ShopifyProductGet(long? productId)
        {
            SqlParameter[] parameters = { new SqlParameter("@ProductId", productId) };
            var data = _dbHelper.ExecuteProcedure("ShopifyProductGet", parameters);
            if (data.Rows.Count > 0)
            {
                long parentProductId;
                long.TryParse(data.Rows[0]["ParentShopifyId"].ToString(), out parentProductId);
                int variantsCount;
                int.TryParse(data.Rows[0]["VariantsCount"].ToString(), out variantsCount);
                ShopifyProductModel model = new ShopifyProductModel
                {
                    Id = int.Parse(data.Rows[0]["Id"].ToString()),
                    ShopifyId = long.Parse(data.Rows[0]["ShopifyId"].ToString()),
                    Title = data.Rows[0]["Title"].ToString(),
                    ProductType = data.Rows[0]["ProductType"].ToString(),
                    Handle = data.Rows[0]["ProductType"].ToString(),
                    VariantsCount = variantsCount,
                    ParentShopifyId = parentProductId
                };
                return model;
            }
            else
            {
                return new ShopifyProductModel();
            }
        }

        /// <summary>
        /// Method to Get Product based on Parent Shopify Product ID
        /// </summary>
        /// <param name="parentProductId">Parent Shopify Product Id</param>
        /// <returns>Product Model</returns>
        public ShopifyProductModel ShopifyProductByParentProductGetLast(long? parentProductId)
        {
            SqlParameter[] parameters = { new SqlParameter("@ParentProductId", parentProductId) };
            var data = _dbHelper.ExecuteProcedure("ShopifyProductByParentProductGetLast", parameters);
            if (data.Rows.Count > 0)
            {
                int variantsCount;
                int.TryParse(data.Rows[0]["VariantsCount"].ToString(), out variantsCount);
                ShopifyProductModel model = new ShopifyProductModel
                {
                    Id = int.Parse(data.Rows[0]["Id"].ToString()),
                    ShopifyId = long.Parse(data.Rows[0]["ShopifyId"].ToString()),
                    Title = data.Rows[0]["Title"].ToString(),
                    ProductType = data.Rows[0]["ProductType"].ToString(),
                    Handle = data.Rows[0]["ProductType"].ToString(),
                    VariantsCount = variantsCount,
                    ParentShopifyId = parentProductId ?? 0
                };
                return model;
            }
            else
            {
                return new ShopifyProductModel();
            }
        }
    }
}