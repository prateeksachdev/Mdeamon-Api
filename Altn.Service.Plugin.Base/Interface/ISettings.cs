using System;

namespace Altn.Service.Plugin.Base.Interface
{
    public interface ISettings
    {
        string ConnectionString { get; }
        string ShopifyUrl { get; }
        string ShopAccessToken { get; }
        string ShopWebhookSecret { get; }
        int SyncInterval { get; }
        int NumberOfDaysOld { get; }
        bool DebugMode { get; }
        bool SyncExistingProducts { get; }
        DateTime LastExecuted { get; }

        /// <summary>
        /// Custom plugin settings.
        /// </summary>
        T GetPluginSettings<T>();
    }
}
