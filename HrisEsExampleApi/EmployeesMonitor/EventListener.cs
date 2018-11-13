using System;
using EventStore.ClientAPI;

namespace EmployeesMonitor
{
    public class EventListener : IDisposable
    {
        private IEventStoreConnection connection;


        public void Start()
        {
            connection = EventStoreConnection.Create("ConnectTo=tcp://HrisDemo:N&mD8Le5OWv0@qa-sandbox-114:1113");

            connection.ConnectAsync().Wait();

            var settings = new CatchUpSubscriptionSettings(
                int.MaxValue,
                1,
                false,
                true);
            connection.SubscribeToStreamFrom(
                "Employee",
                0L,
                settings,
                (subscription, evt) => ProcessEvent(evt));

            connection.SubscribeToStreamAsync(
                    "Employee",
                    true,
                    (subscription, evt) => ProcessEvent(evt))
                .Wait();
        }

        private static void ProcessEvent(ResolvedEvent evt)
        {
            var type = evt.Event.EventType;
            Console.WriteLine($"  Received event of type, {type}, with event number, {evt.OriginalEventNumber}.");
        }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}