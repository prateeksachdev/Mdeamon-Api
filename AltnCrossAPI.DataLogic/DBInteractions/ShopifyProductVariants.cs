using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class ShopifyProductVariants : IShopifyProductVariants
    {
        private readonly DBHelper _dbHelper;
        public ShopifyProductVariants()
        {
            _dbHelper = new DBHelper();
        }
        public void ShopifyProductVariantsDelete(string whereClause, long productId)
        {
            SqlParameter[] parameters = { new SqlParameter("@WhereClause", whereClause),
            new SqlParameter("@ProductId", productId)
            };
            _dbHelper.ExecuteNonQuery("ShopifyProductVariantsDelete", parameters);
        }

        public long ShopifyProductVariantIdGet(long productId, decimal price)
        {
            SqlParameter[] parameters = { new SqlParameter("@ProductId", productId),
            new SqlParameter("@Price", price)
            };

            return _dbHelper.ExecuteReaderQuery<long>("exec ShopifyProductVariantIdGet @ProductId, @Price", parameters);
        }

        public DataTable ShopifyProductVariantsByProductIdGet(long productId)
        {
            SqlParameter[] parameters = { new SqlParameter("@ProductId", productId) };
            return _dbHelper.ExecuteProcedure("ShopifyProductVariantsByProductIdGet", parameters);
        }

        public void ShopifyProductVariantInsertUpdate(ShopifyProductVariantModel model)
        {
            SqlParameter[] parameters = { new SqlParameter("@ShopifyId", model.ShopifyId),
            new SqlParameter("@ProductId", model.ProductId),
            new SqlParameter("@CompareAtPrice", model.CompareAtPrice),
            new SqlParameter("@Price", model.Price),
            new SqlParameter("@Barcode", model.Barcode),
            new SqlParameter("@SKU", model.SKU),
            new SqlParameter("@Title", model.Title),
            new SqlParameter("@Weight", model.Weight),
            new SqlParameter("@WeightUnit", model.WeightUnit),
            new SqlParameter("@InventoryQuantity", model.InventoryQuantity),
            new SqlParameter("@InventoryItemId", model.InventoryItemId)
            };

            _dbHelper.ExecuteNonQuery("ShopifyProductVariantInsertUpdate", parameters);
        }
    }
}