using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Counter.Dal.Gateways.CounterServiceAgent;
using Counter.Domain.Gateways.CounterServiceAgent;

namespace Counter.Web.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.Register<CounterServiceAgent>(e => new CounterServiceAgent(new HttpClient()
                {
                    BaseAddress = new Uri(WebConfigurationManager.AppSettings["CountServiceBaseAddress"])
                }))
                .As<ICounterServiceAgent>();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}