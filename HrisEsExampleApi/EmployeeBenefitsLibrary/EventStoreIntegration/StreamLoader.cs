using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public class StreamLoader : IStreamLoader
    {
        private readonly IConverter converter;
        private readonly IEventStoreReadProxy proxy;
        private readonly IEventStoreConnection connection;

        public StreamLoader(
            IConverter converter,
            IEventStoreReadProxy proxy,
            IEventStoreConnection connection)
        {
            this.converter = converter;
            this.proxy = proxy;
            this.connection = connection;
        }

        public async Task<IEnumerable<Event>> LoadEvents(string stream, TypeMap types)
        {
            await connection.ConnectAsync();

            var version = 0L;
            var events = new List<Event>();

            while (true)
            {
                // todo, the contents of this loop should be put into a dependency
                if (!(await proxy.ReadEventAsync(connection, stream, version) is LoadedEvent eventFromStream))
                    break;

                var evt = converter.ConvertReadToDomain(types, eventFromStream);

                events.Add(evt);

                version++;
            }

            connection.Close();

            return events;
        }
    }
}