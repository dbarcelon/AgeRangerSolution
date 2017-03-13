using System;
using System.Collections.Generic;
using Castle.Windsor;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace AgeRanger.API.CastleWindsor
{
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer container;

        public WindsorCompositionRoot(IWindsorContainer container)
        {
            this.container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController)this.container.Resolve(controllerType);

            request.RegisterForDispose(new Release(() => this.container.Release(controller)));

            return controller;
        }
    }
}