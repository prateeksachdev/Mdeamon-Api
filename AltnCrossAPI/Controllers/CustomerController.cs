using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Shared;
using AltnCrossAPI.Shared.Logging;
using Newtonsoft.Json;
using ShopifySharp;
using System.Threading.Tasks;
using System.Web.Http;
using static AltnCrossAPI.Shared.Enums;

namespace AltnCrossAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomersBL _customerBL;

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
        [ValidateShopifyRequest]
        [ActionName("CustomerSync")]
        public string UserProfile(object userJson)
        {
            Customer customer = JsonConvert.DeserializeObject<Customer>(userJson.ToString());
            return _customerBL.CustomerSync(customer).Message;
        }

        /// <summary>
        /// Action method to delete customer from db
        /// </summary>
        /// <param name="userJson">Customer Json posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        [HttpPost]
        [ValidateShopifyRequest]
        [ActionName("CustomerDelete")]
        public string DeleteUser(object userJson)
        {
            Customer customer = JsonConvert.DeserializeObject<Customer>(userJson.ToString());
            return _customerBL.CustomerDelete(customer).Message;
        }
    }
}
