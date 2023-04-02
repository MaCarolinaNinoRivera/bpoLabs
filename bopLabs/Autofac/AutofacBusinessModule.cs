using System.Reflection;
using Autofac;
using bpoLabs.Business;
using bpoLabs.Common.Database;
using bpoLabs.Repository;
using Module = Autofac.Module;

namespace bpoLabs.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var repositoriesAssembly = Assembly.GetAssembly(typeof(EmployeesRepository));
            builder.RegisterAssemblyTypes(repositoriesAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            var servicesAssembly = Assembly.GetAssembly(typeof(EmployeesService));
            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            var DapperWrapperAssembly = Assembly.GetAssembly(typeof(DapperWrapper));
            builder.RegisterAssemblyTypes(servicesAssembly)
                .AsImplementedInterfaces();
        }
    }
}
