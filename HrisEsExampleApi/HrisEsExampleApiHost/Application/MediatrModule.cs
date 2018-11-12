using Autofac;
using EmployeeBenefitsLibrary;
using MediatR;

namespace HrisEsExampleApiHost.Application
{
    public class MediatrModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();

            var openTypes = new[]
            {
                typeof(IRequestHandler<,>)
            };

            foreach (var openType in openTypes)
                builder
                    .RegisterAssemblyTypes(typeof(HireEmployee).Assembly)
                    .AsClosedTypesOf(openType)
                    .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}