using Autofac;
using Autofac.Integration.Mvc;
using SSW.Data.Contexts;
using SSW.Data.Repositories;
using System.Web.Mvc;

namespace SSW.Web.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<UniversityDbContext>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}