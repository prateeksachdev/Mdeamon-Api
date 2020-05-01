using AltnCrossAPI.BusinessLogic;
using AltnCrossAPI.Database;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace AltnCrossAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var container = new Container();
            container.Register<IShopifyOrders, ShopifyOrder>();
            container.Register<IShopifyData, ShopifyData>();
            container.Register<IShopifyOrderAddresses, ShopifyOrderAddresses>();
            container.Register<IShopifyOrderLineItems, ShopifyOrderLineItems>();
            container.Register<IRegKeys, RegKeys>();

            container.Register<ICustomersBL, CustomersBL>();
            container.Register<IOrdersBL, OrdersBL>();
            container.Register<IProductsBL, ProductsBL>();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);


            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
