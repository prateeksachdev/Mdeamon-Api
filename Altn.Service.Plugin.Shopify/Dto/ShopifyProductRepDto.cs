using System;

namespace Altn.Service.Plugin.Shopify.Dto
{
    public class ShopifyProductDto
    {
        public int Id { get; set; }
        public long ShopifyId { get; set; }
        public string Handle { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}