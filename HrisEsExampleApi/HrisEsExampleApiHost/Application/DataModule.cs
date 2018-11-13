using Autofac;
using EmployeeDataLibrary;
using MediatR;

namespace HrisEsExampleApiHost.Application
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types = new[]
            {
                typeof(IRequestHandler<,>)
            };

            var assembly = typeof(Employee).Assembly;

            foreach (var type in types)
            {
                builder
                    .RegisterAssemblyTypes(assembly)
                    .AsClosedTypesOf(type)
                    .AsImplementedInterfaces();
            }

            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces();
        }
    }
}