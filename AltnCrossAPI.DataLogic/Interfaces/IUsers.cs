using AltnCrossAPI.Database.Models;

namespace AltnCrossAPI.Database.Interfaces
{
    public interface IUsers
    {
        void UserShopifyCustomerIDUpdate(UsersModel model);

        void UserDeleteByShopifyCustomerID(long? shopifyCustomerId);

        UsersModel UserGetByShopifyCustomerID(long? shopifyCustomerId);
    }
}