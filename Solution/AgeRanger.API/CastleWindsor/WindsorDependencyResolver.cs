using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace AgeRanger.API.CastleWindsor
{
    public class WindsorDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IWindsorContainer windsorContainer;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Container cannot be null");
            }
            windsorContainer = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(windsorContainer);
            //return null;
        }

        public object GetService(Type serviceType)
        {
            return windsorContainer.Kernel.HasComponent(serviceType) ? windsorContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return windsorContainer.ResolveAll(serviceType).Cast<object>().ToArray();
        }

        public void Dispose()
        {
            windsorContainer.Dispose();
        }
    }
}