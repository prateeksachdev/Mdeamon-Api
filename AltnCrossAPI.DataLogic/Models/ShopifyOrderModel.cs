using System;

namespace AltnCrossAPI.Database.Models
{
    public class ShopifyOrderModel 
    {
        #region Class Property Declarations

        public int Id { get; set; }

        public long ShopifyId { get; set; }

        public int ShopifyDataId { get; set; }

        public int OrderNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime ProcessedOn { get; set; }

        public string Token { get; set; }

        public string CheckoutToken { get; set; }

        public string Gateway { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal SubTotalPrice { get; set; }

        public decimal TotalTax { get; set; }

        public string FinancialStatus { get; set; }

        public string ProcessingMethod { get; set; }

        public string Currency { get; set; }

        public long CheckoutId { get; set; }

        public long AppId { get; set; }

        public string BrowserIP { get; set; }

        public string OrderStatusUrl { get; set; }
        #endregion
    }
}