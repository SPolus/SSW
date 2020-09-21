using Autofac;
using Autofac.Integration.Mvc;
using SSW.Data.Contexts;
using SSW.Data.Repositories;
using SSW.Web.Services;
using System.Web.Mvc;

namespace SSW.Web.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            builder.RegisterType<UniversityDbContext>().InstancePerRequest();
            builder.RegisterType<CookieService>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}