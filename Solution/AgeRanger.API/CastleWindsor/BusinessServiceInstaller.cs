using Castle.MicroKernel.Registration;

namespace AgeRanger.API.CastleWindsor
{
    public class BusinessServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<AgeRanger.Business.IBusinessService.IPersonService>().ImplementedBy<AgeRanger.Business.BusinessService.PersonService>().LifestylePerWebRequest());
            container.Register(Component.For<AgeRanger.Business.IBusinessService.IAgeGroupService>().ImplementedBy<AgeRanger.Business.BusinessService.AgeGroupService>().LifestylePerWebRequest());
        }
    }
}