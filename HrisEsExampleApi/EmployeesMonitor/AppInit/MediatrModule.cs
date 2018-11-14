using Autofac;
using MediatR;

namespace EmployeesMonitor.AppInit
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
                    .RegisterAssemblyTypes(ThisAssembly)
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