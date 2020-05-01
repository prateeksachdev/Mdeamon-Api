using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Mvc;

namespace AltnCrossAPI
{
    public class SimpleInjectorDependencyResolver : System.Web.Mvc.IDependencyResolver,
        System.Web.Http.Dependencies.IDependencyResolver,
        IDependencyScope
    {
        public Container _container { get; private set; }
        public SimpleInjectorDependencyResolver(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this._container = container;
        }

        public object GetService(Type serviceType)
        {
            if(!serviceType.IsAbstract && typeof(IController).IsAssignableFrom(serviceType))
            {
                return this._container.GetInstance(serviceType);
            }
            return ((IServiceProvider)this._container).GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._container.GetAllInstances(serviceType);
        }
        IDependencyScope System.Web.Http.Dependencies.IDependencyResolver.BeginScope()
        {
            return this;
        }
        object IDependencyScope.GetService(Type serviceType)
        {
            return ((IServiceProvider)this._container)
                .GetService(serviceType);
        }
        IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
        {
            IServiceProvider provider = this._container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var services = (IEnumerable<object>)provider.GetService(collectionType);
            return services ?? Enumerable.Empty<object>();
        }
        void IDisposable.Dispose() { }
    }
}