using ShopifySharp;
using System.Threading.Tasks;

namespace AltnCrossAPI.BusinessLogic.Interfaces
{
    public interface IOrdersBL
    {
        Task<string> OrderSync(Order order);
    }
}