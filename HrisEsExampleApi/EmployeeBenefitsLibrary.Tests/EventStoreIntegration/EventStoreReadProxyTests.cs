using EventStore.ClientAPI;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests.EventStoreIntegration
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