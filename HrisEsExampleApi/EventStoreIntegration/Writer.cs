using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeBenefitsLibrary;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EventStoreIntegration
{
    public class Writer : IWriter
    {
        private readonly IEventStoreConnection connection;
        private readonly ITypeMapSelector selector;

        public Writer(IEventStoreConnection connection, ITypeMapSelector selector)
        {
            this.connection = connection;
            this.selector = selector;
        }

        // todo, wrap a test or three around this method.
        public async Task Write<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregate
        {
            var typeMap = selector.For(typeof(TAggregate));

            var eventData = ConvertEventsToEventData(aggregate.NewEvents, typeMap);

            var stream = $"{aggregate.StreamName}.{aggregate.Id}";

            await connection.ConnectAsync();

            await connection.AppendToStreamAsync(
                stream,
                ExpectedVersion.Any,
                eventData);

            connection.Close();
        }

        private static IEnumerable<EventData> ConvertEventsToEventData(IEnumerable<Event> events, TypeMap typeMap)
        {
            return from newEvent in events
                let name = typeMap.Single(o => o.type == newEvent.GetType()).name
                let json = JsonConvert.SerializeObject(newEvent)
                let data = Encoding.UTF8.GetBytes(json)
                select new EventData(
                    Guid.NewGuid(),
                    name,
                    true,
                    data,
                    null);
        }
    }


}