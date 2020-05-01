using ShopifySharp;
using System.Threading.Tasks;

namespace AltnCrossAPI.BusinessLogic
{
    public interface IOrdersBL
    {
        Task<string> OrderSync(Order order);
    }
}