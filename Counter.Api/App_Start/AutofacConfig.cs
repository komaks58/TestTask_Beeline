using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Counter.Dal.Repositories;
using Counter.Domain.Helpers;
using Counter.Domain.Logic.Count;
using Counter.Domain.Repositories;

namespace Counter.Api
{
    public class AutofacConfig
    {
        public static void ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CalculationRepository>().As<ICalculationRepository>();
            builder.RegisterType<CountLogic>().As<ICountLogic>();
            builder.RegisterType<Utf8XmlSerializer>().As<IXmlSerializer>().SingleInstance();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}