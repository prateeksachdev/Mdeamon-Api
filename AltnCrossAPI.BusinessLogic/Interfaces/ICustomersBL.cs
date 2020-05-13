using AltnCrossAPI.Shared;
using ShopifySharp;

namespace AltnCrossAPI.BusinessLogic.Interfaces
{
    public interface ICustomersBL
    {
        Result CustomerSync(Customer customer);
        Result CustomerDelete(Customer customer);
    }
}