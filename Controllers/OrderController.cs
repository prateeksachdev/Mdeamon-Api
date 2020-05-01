using AltnCrossAPI.BusinessLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopifySharp;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using static AltnCrossAPI.BusinessLogic.Enums;

namespace AltnCrossAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderController : ApiController
    {
        private IOrdersBL _ordersBL;

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
        [Route("OrderSync")]
        public async Task<string> Order(object orderJson)
        {
            //check validity if request is from shopify store or not
            if (await General.isReuqestAuthentic())
            {
                Order order;

                var obj = JObject.Parse(orderJson.ToString());
                //check if json's root node is order or not
                if (obj.Properties().Select(p => p.Name).FirstOrDefault() == "order")
                {
                    order = obj.Properties().Select(p => p.Value).FirstOrDefault().ToObject<Order>();
                }
                else
                {
                    order = JsonConvert.DeserializeObject<Order>(orderJson.ToString());
                }
                return await _ordersBL.OrderSync(order);
            }
            else
                return ToStringEnums(OrderHttpCustomResponse.AuthenticationError);
        }
    }
}
