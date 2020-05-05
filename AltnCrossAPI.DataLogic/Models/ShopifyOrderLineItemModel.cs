using System;

namespace AltnCrossAPI.Database.Models
{
    public class ShopifyOrderLineItemModel
    {
        #region Class Property Declarations

        public int Id { get; set; }

        public long ShopifyId { get; set; }

        public long OrderId { get; set; }

        public long VariantId { get; set; }

        public string Title { get; set; }

        public int Quantity { get; set; }

        public int FulfillableQuantity { get; set; }

        public string SKU { get; set; }

        public string Vendor { get; set; }

        public long ProductId { get; set; }

        public decimal Price { get; set; }

        public decimal TotalDiscount { get; set; }

        public string TaxCode { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateUpdated { get; set; }

        #endregion

    }
}