using Autofac;
using EmployeeBenefitsLibrary;
using EventStore.ClientAPI;
using EventStoreIntegration;

namespace HrisEsExampleApiHost.Application
{
    public class EventStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(context => EventStoreConnection.Create("ConnectTo=tcp://HrisDemo:N&mD8Le5OWv0@qa-sandbox-114:1113"))
                .As<IEventStoreConnection>()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(Employee).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(NoEventFound).Assembly)
                .AsImplementedInterfaces();
        }
    }
}