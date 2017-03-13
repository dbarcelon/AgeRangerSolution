using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace AgeRanger.API.CastleWindsor
{
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer windsorContainer;
        private readonly IDisposable disposableScope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Container cannot be null");
            }
            windsorContainer = container;
            disposableScope = windsorContainer.BeginScope();
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
            disposableScope.Dispose();
        }
    }
}