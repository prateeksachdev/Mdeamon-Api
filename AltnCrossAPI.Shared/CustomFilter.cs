using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static AltnCrossAPI.Shared.Enums;

namespace AltnCrossAPI.Shared
{
    public class ValidateShopifyRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var requestHeaders = HttpContext.Current.Request.Headers.ToKvps();
            Stream inputStream = HttpContext.Current.Request.InputStream;
            string requestBody = null;
            inputStream.Position = 0;
            using (StreamReader reader = new StreamReader(inputStream))
            {
                requestBody = reader.ReadToEnd();
            }

            var hmacHeaderValues = requestHeaders.FirstOrDefault(kvp => kvp.Key.Equals("X-Shopify-Hmac-SHA256", StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrEmpty(hmacHeaderValues) || hmacHeaderValues.Count() < 1)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(ToStringEnums(OrderHttpCustomResponse.AuthenticationError)), ReasonPhrase = ToStringEnums(OrderHttpCustomResponse.AuthenticationError) };
                base.OnActionExecuting(context);
                return;
            }

            //Compute a hash from the apiKey and the request body
            string hmacHeader = hmacHeaderValues.First();
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ConfigHelper.ShopWebhookSecret));
            string hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody)));

            //Webhook is valid if computed hash matches the header hash
            if(!(hash == hmacHeader))
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(ToStringEnums(OrderHttpCustomResponse.AuthenticationError)), ReasonPhrase = ToStringEnums(OrderHttpCustomResponse.AuthenticationError) };
                base.OnActionExecuting(context);
                return;
            }
        }
    }
}