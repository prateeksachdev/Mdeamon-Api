using System;

namespace Altn.Service.Plugin.Shopify.Dto
{
    public class ShopifyProductVariantsDto
    {
        public int Id { get; set; }
        public long ShopifyId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}