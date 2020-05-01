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
    public class CustomerController : ApiController
    {
        private ICustomersBL _customerBL;

        public CustomerController(ICustomersBL customerBL)
        {
            _customerBL = customerBL;
        }

        /// <summary>
        /// Action method to Insert a new shopify customer or Update existing shopify customer in db
        /// </summary>
        /// <param name="userJson">Customer Json posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        [HttpPost]
        [Route("CustomerSync")]
        public async Task<string> UserProfile(object userJson)
        {
            //check validity if request is from shopify store or not
            if (await General.isReuqestAuthentic())
            {
                Customer customer;

                var obj = JObject.Parse(userJson.ToString());
                //check if json's root node is customer or not
                if (obj.Properties().Select(p => p.Name).FirstOrDefault() == "customer")
                {
                    customer = obj.Properties().Select(p => p.Value).FirstOrDefault().ToObject<Customer>();
                }
                else
                {
                    customer = JsonConvert.DeserializeObject<Customer>(userJson.ToString());
                }
                return _customerBL.CustomerSync(customer).Message;
            }
            else
                return ToStringHttpCustomResponse(OrderHttpCustomResponse.AuthenticationError);
        }
    }
}
