using System.Configuration;

namespace AltnCrossAPI.Helper
{
    public static class ConfigHelper
    {
        public static string ShopifyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopifyUrl"].ToString();
            }
        }

        public static string ShopAccessToken
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopAccessToken"].ToString();
            }
        }

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MDDB"].ToString();
            }
        }

        public static long LocationID
        {
            get
            {
                return long.Parse(ConfigurationManager.AppSettings["LocationID"].ToString());
            }
        }

        public static string ShopWebhookSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopWebhookSecret"].ToString();
            }
        }
    }
}