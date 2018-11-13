using EventStore.ClientAPI;
using Xunit;

namespace EventStoreIntegration.Tests
{
    public class EventStoreReadProxyTests
    {
        [Fact(Skip = "ignore this")]
        public async void ItShouldConnect()
        {
            var connection = EventStoreConnection.Create("ConnectTo=tcp://hirsdemo:mD8Le5OWv0@qa-sandbox-114:1113");

            await connection.ConnectAsync();

            connection.Dispose();
        }
    }
}