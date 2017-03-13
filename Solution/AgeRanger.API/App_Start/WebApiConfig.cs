using Castle.Windsor;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace AgeRanger.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {
            //Setting the configuration for Json Formatting
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            if (!config.Routes.ContainsKey("DefaultApi"))
            {
                config.Routes.MapHttpRoute(
                   name: "DefaultApi",
                   routeTemplate: "api/{controller}/{action}/{param}",
                   defaults: new { param = RouteParameter.Optional });
            }

            RegisterControllerActivator(container);
        }

        public static void RegisterControllerActivator(IWindsorContainer container)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new AgeRanger.API.CastleWindsor.WindsorCompositionRoot(container));
        }
    }
}
