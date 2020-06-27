using System;
using System.Configuration;

namespace Altn.Service
{
    public class GlobalSettings
    {
        public static string ConnectionString
        {
            get
            {
                var data = ConfigurationManager.ConnectionStrings["dbConnection"];

                if (data == null || string.IsNullOrWhiteSpace(data.ConnectionString))
                    throw new Exception("Failed to get connectionstring from settings.");

                return data.ConnectionString;
            }
        }

        public static string GetSetting(string key)
        {
            var data = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(data))
                throw new Exception("Failed to read: " + key + ", from settings.");

            return data.ToString();
        }

        public static bool GetSettingBool(string key)
        {
            var data = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(data))
                throw new Exception("Failed to read: " + key + ", from settings.");

            if (!bool.TryParse(data, out bool result))
                throw new Exception("Failed to convert value: " + data + ", to boolean.");

            return result;
        }

        public static int GetSettingInt(string key)
        {
            var result = 0;
            var data = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(data))
                throw new Exception("Failed to read: " + key + ", from settings.");

            if (!int.TryParse(data, out result))
                throw new Exception("Failed to convert value: " + data + ", to integer.");

            return result;
        }
    }
}