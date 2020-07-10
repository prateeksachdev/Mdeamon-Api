using System;

namespace AltnCrossAPI.Database.Models
{
    public class ShopifyProductVariantModel
    {
        #region Class Property Declarations

        public int Id { get; set; }

        public long ShopifyId { get; set; }

        public long ProductId { get; set; }

        public decimal? CompareAtPrice { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public decimal? Price { get; set; }

        public decimal? Weight { get; set; }

        public string Title { get; set; }

        public string Barcode { get; set; }

        public int? InventoryQuantity { get; set; }

        public long? InventoryItemId { get; set; }

        public string SKU { get; set; }

        public string WeightUnit { get; set; }

        #endregion

    }
}