using System;
using Altn.Service.Plugin.Base.Interface;

namespace Altn.Service.Domain
{
    public class Settings : ISettings
    {
        public string ConnectionString => GlobalSettings.ConnectionString;
        public string ShopifyUrl => GlobalSettings.GetSetting("ShopifyUrl");
        public string ShopAccessToken => GlobalSettings.GetSetting("ShopAccessToken");
        public string ShopWebhookSecret => GlobalSettings.GetSetting("ShopWebhookSecret");
        public bool DebugMode => GlobalSettings.GetSettingBool("DebugMode");
        public bool SyncExistingProducts => GlobalSettings.GetSettingBool("SyncExistingProducts");
        public int SyncInterval => GlobalSettings.GetSettingInt("SyncInterval");
        public int NumberOfDaysOld => GlobalSettings.GetSettingInt("NumberOfDaysOld");
        public DateTime LastExecuted => DateTime.MinValue;

        public T GetPluginSettings<T>()
        {
            throw new NotImplementedException();
        }
    }
}