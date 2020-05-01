using altn_common.Catalog;
using altn_common.KeyCodes;
using altn_common.Profiles;

namespace AltnCrossAPI.Database.ViewModels
{
    public class BaseViewModels
    {
        public string ErrorMessage { get; set; }
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
    public class UserProfileViewModel: BaseViewModels
    {
        public UserProfile User { get; set; }
    }
}