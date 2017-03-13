using Castle.MicroKernel.Registration;

namespace AgeRanger.API.CastleWindsor
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<AgeRanger.Data.IRepository.IPersonRepository>().ImplementedBy<AgeRanger.Data.Repository.PersonRepository>().LifestylePerWebRequest());
            container.Register(Component.For<AgeRanger.Data.IRepository.IAgeGroupRepository>().ImplementedBy<AgeRanger.Data.Repository.AgeGroupRepository>().LifestylePerWebRequest());
        }
    }
}