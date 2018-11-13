using System.Collections.Generic;
using EmployeeBenefitsLibrary.EventStoreIntegration;
using EventStore.ClientAPI;
using FakeItEasy;
using Xunit;

namespace EmployeeBenefitsLibrary.Tests.EventStoreIntegration
{
    public class StreamLoaderTests
    {
        private readonly IStreamLoader sut;
        private readonly IEventStoreConnection connection;
        private readonly IEventStoreReadProxy proxy;
        private readonly IConverter converter;

        public StreamLoaderTests()
        {
            converter = A.Fake<IConverter>();
            proxy = A.Fake<IEventStoreReadProxy>();
            connection = A.Fake<IEventStoreConnection>();

            sut = new StreamLoader(
                converter,
                proxy,
                connection);
        }

        [Fact]
        public async void ItLoadsEventsFromTheStream()
        {
            var loaded = new List<EventFromStream>();
            loaded.AddRange(A.CollectionOfDummy<LoadedEvent>(5));
            loaded.Add(A.Dummy<NoEventFound>());

            const string stream = "stream";
            var typeMap = new TypeMap();
            A.CallTo(() => proxy.ReadEventAsync(connection, stream, A<long>._))
                .ReturnsNextFromSequence(loaded.ToArray());

            await sut.LoadEvents(stream, typeMap);

            A.CallTo(() => connection.ConnectAsync()).MustHaveHappened()
                .Then(A.CallTo(() => connection.Close()).MustHaveHappened());
            A.CallTo(() => converter.ConvertReadToDomain(typeMap, A<LoadedEvent>._))
                .MustHaveHappened();
        }
    }
}