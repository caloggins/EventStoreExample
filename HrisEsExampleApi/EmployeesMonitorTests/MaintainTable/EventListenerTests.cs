using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeesMonitor.EventStoreIntegration;
using EmployeesMonitor.MaintainTable;
using EventStore.ClientAPI;
using FakeItEasy;
using Xunit;

namespace EmployeesMonitorTests.MaintainTable
{
    public class EventListenerTests
    {
        private readonly IEventListener sut;
        private readonly IEventStoreConnection connection;

        public EventListenerTests()
        {
            connection = A.Fake<IEventStoreConnection>();
            var processor = A.Fake<IProcessor>();
            sut = new EventListener(connection, processor);
        }

        [Fact]
        public void Start_ProcessesEventsFromStream()
        {
            sut.Start();

            A.CallTo(() => connection.ConnectAsync()).MustHaveHappened()
                .Then(A.CallTo(() => connection.SubscribeToStreamAsync(
                    "Employee",
                    true,
                    A<Func<EventStoreSubscription, ResolvedEvent, Task>>._,
                    null,
                    null)).MustHaveHappened());
        }
    }
}