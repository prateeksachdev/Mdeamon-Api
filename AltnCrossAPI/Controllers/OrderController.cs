using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Shared.Logging;
using Newtonsoft.Json;
using AltnCrossAPI.Shared;
using ShopifySharp;
using System.Threading.Tasks;
using System.Web.Http;
using static AltnCrossAPI.Shared.Enums;
using System.Net;

namespace AltnCrossAPI.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrdersBL _ordersBL;

        public OrderController(IOrdersBL ordersBL)
        {
            _ordersBL = ordersBL;
        }

        /// <summary>
        /// Action method to Insert a new shopify order or Update existing shopify order in db
        /// and mark it complete after adding keys to shopify order NoteAttribute
        /// </summary>
        /// <param name="orderJson">Order Json posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        [HttpPost]
        [ValidateShopifyRequest]
        [ActionName("OrderSync")]
        public async Task<string> Order(object orderJson)
        {
            Order order = JsonConvert.DeserializeObject<Order>(orderJson.ToString());
            return await _ordersBL.OrderSync(order);
        }

        /// <summary>
        /// Action method to Inserts/update a new shopify cart PO Wire Number
        /// </summary>
        /// <param name="model">ShopifyCartPOWireNoModel</param>
        /// <returns>Returns status of the request after processing</returns>
        [HttpPost]
        [ActionName("CartPOWireNoSync")]
        public HttpStatusCode CartPOWireNoSync(object model)
        {
            return _ordersBL.CartPOWireNoSync(model);
        }
    }
}
