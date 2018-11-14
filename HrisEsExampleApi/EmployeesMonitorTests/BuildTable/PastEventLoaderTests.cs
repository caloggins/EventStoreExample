using System;
using System.Threading.Tasks;
using EmployeesMonitor.BuildTable;
using EmployeesMonitor.EventStoreIntegration;
using EventStore.ClientAPI;
using FakeItEasy;
using Xunit;

namespace EmployeesMonitorTests.BuildTable
{
    public class PastEventLoaderTests
    {
        private readonly IPastEventLoader sut;
        private readonly IEventStoreConnection connection;

        public PastEventLoaderTests()
        {
            connection = A.Fake<IEventStoreConnection>();
            var processor = A.Fake<IProcessor>();
            sut = new PastEventLoader(connection, processor);
        }

        [Fact]
        public void Run_PassesEventsToProcessor()
        {
            sut.Run();

            A.CallTo(() => connection.ConnectAsync()).MustHaveHappened()
                .Then(A.CallTo(() => connection.SubscribeToStreamFrom(
                    "Employee",
                    0L,
                    sut.Settings,
                    A<Func<EventStoreCatchUpSubscription, ResolvedEvent, Task>>._,
                    null,
                    null,
                    null)).MustHaveHappened());
        }
    }
}