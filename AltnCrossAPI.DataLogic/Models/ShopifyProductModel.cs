using System;

namespace AltnCrossAPI.Database.Models
{
    public class ShopifyProductModel
    {
        #region Class Property Declarations

        public int Id { get; set; }

        public long ShopifyId { get; set; }

        public int ShopifyDataId { get; set; }

        public string Title { get; set; }

        public string Tags { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int VariantsCount { get; set; }

        public string ProductType { get; set; }

        public string Handle { get; set; }

        public string Meta { get; set; }

        public long ParentShopifyId { get; set; }

        public string BodyHtml { get; set; }
        #endregion
    }
}