
namespace AltnCrossAPI.Database.ViewModels
{
    public class BaseViewModels
    {
        public string ErrorMessage { get; set; }
        public string AdditionalInfo { get; set; }
    }
    public class UnitPriceResponseViewModel: BaseViewModels
    {
        public object priceResponse { get; set; }
    }
    public class KeyValidityViewModel: BaseViewModels
    {
        public KeyValidityViewModel()
        {
            isValid = false;
        }
        public bool isValid { get; set; }
    }
    public class CustomVariantViewModel
    {
        public long ProductId { get; set; }
        public long VariantId { get; set; }
        public decimal Price { get; set; }
    }
    public class ResponseViewModel: BaseViewModels
    {
        public object response { get; set; }
    }
}