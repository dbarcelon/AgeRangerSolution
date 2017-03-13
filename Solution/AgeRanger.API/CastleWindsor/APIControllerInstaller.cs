using Castle.MicroKernel.Registration;
using System.Web.Http;

namespace AgeRanger.API.CastleWindsor
{
    public class APIControllerInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());
        }
    }
}