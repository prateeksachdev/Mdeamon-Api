using System;
using System.IO;
using System.Linq;
using System.Threading;
using Altn.Service.Plugin.Base.Interface;
using Altn.Service.Plugin.Shopify.DataAccess;
using ShopifySharp;

namespace Altn.Service.Plugin.Shopify
{
    public class Start : IPlugin
    {
        private ILog _Log;
        private ISettings _settings;
        public Start() { }

        public string PluginName
        {
            get
            {
                return "Custom";
            }
        }

        async void IPlugin.Start(ILog log, ISettings settings)
        {
            try
            {
                _Log = log;
                _settings = settings;

                if (_settings.SyncExistingProducts)//Sync already existing products from shopify to db
                {
                    ProductService productService = new ProductService(_settings.ShopifyUrl, _settings.ShopAccessToken);
                    var pList = await productService.ListAsync();

                    _Log.Info("Syncing Products :: " + pList.Items.Count());

                    foreach (Product p in pList.Items)
                    {
                        await productService.UpdateAsync(p.Id ?? 0, p);
                    }
                }

                var variants = ShopifyProductVariantsRep.Get(_settings.ConnectionString, _settings.NumberOfDaysOld);

                if (variants == null || variants.Count() == 0)
                    return;

                _Log.Info("Variants Count :: " + variants.Count());

                foreach (var variant in variants)
                {
                    try
                    {
                        _Log.Info("Deleting Variant :: " + variant.ProductId + " :: " + variant.ShopifyId);
                        ProductVariantService variantService = new ProductVariantService(_settings.ShopifyUrl, _settings.ShopAccessToken);
                        variantService.DeleteAsync(variant.ProductId, variant.ShopifyId).Wait();
                    }
                    catch (Exception exp)
                    {
                        _Log.Error("Delete Old Resource :: " + exp.Message, exp);
                    }
                }
            }
            catch (Exception exp)
            {
                _Log.Error("Start :: " + exp.Message, exp);
            }
        }
    }
}