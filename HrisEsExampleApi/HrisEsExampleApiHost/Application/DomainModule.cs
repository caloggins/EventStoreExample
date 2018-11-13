using Autofac;
using EmployeeBenefitsLibrary;

namespace HrisEsExampleApiHost.Application
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Employee).Assembly)
                .AsImplementedInterfaces();
        }
    }
}