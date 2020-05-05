using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AltnCrossAPI.Shared
{
    public class General
    {
        public static async Task<bool> isValidShopifyRequest()
        {
            var requestHeaders = HttpContext.Current.Request.Headers.ToKvps();
            Stream inputStream = HttpContext.Current.Request.InputStream;
            string requestBody = null;
            inputStream.Position = 0;
            using (StreamReader reader = new StreamReader(inputStream))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            var hmacHeaderValues = requestHeaders.FirstOrDefault(kvp => kvp.Key.Equals("X-Shopify-Hmac-SHA256", StringComparison.OrdinalIgnoreCase)).Value;

            if (string.IsNullOrEmpty(hmacHeaderValues) || hmacHeaderValues.Count() < 1)
            {
                return false;
            }

            //Compute a hash from the apiKey and the request body
            string hmacHeader = hmacHeaderValues.First();
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ConfigHelper.ShopWebhookSecret));
            string hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody)));

            //Webhook is valid if computed hash matches the header hash
            return hash == hmacHeader;
        }
    }
}