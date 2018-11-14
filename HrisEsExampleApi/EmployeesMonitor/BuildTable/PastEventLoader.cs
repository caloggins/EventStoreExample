using EmployeesMonitor.EventStoreIntegration;
using EventStore.ClientAPI;
using JetBrains.Annotations;

namespace EmployeesMonitor.BuildTable
{
    [UsedImplicitly]
    public class PastEventLoader : IPastEventLoader
    {
        private readonly IEventStoreConnection connection;
        private readonly IProcessor processor;

        public PastEventLoader(
            IEventStoreConnection connection, 
            IProcessor processor)
        {
            this.connection = connection;
            this.processor = processor;
        }

        public CatchUpSubscriptionSettings Settings { get; } =
            new CatchUpSubscriptionSettings(
                int.MaxValue,
                1,
                false,
                true);

        public void Run()
        {
            connection.ConnectAsync()
                .Wait();

            connection.SubscribeToStreamFrom(
                "Employee",
                0L,
                Settings,
                (subscription, evt) => processor.ProcessEvent(evt));
        }

    }
}