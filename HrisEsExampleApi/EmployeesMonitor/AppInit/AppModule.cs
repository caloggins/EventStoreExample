using Autofac;
using EventStore.ClientAPI;

namespace EmployeesMonitor.AppInit
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(context => EventStoreConnection.Create("ConnectTo=tcp://HrisDemo:N&mD8Le5OWv0@qa-sandbox-114:1113"))
                .As<IEventStoreConnection>()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces();
        }
    }
}