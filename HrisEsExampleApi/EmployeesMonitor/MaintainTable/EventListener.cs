using EmployeesMonitor.EventStoreIntegration;
using EventStore.ClientAPI;
using JetBrains.Annotations;

namespace EmployeesMonitor.MaintainTable
{
    public interface IEventListener
    {
        void Start();
    }

    [UsedImplicitly]
    public class EventListener : IEventListener
    {
        private readonly IEventStoreConnection connection;
        private readonly IProcessor processor;

        public EventListener(
            IEventStoreConnection connection,
            IProcessor processor)
        {
            this.connection = connection;
            this.processor = processor;
        }

        public void Start()
        {
            connection.ConnectAsync()
                .Wait();

            connection.SubscribeToStreamAsync(
                    "Employee",
                    true,
                    (subscription, evt) => processor.ProcessEvent(evt))
                .Wait();
        }
    }
}