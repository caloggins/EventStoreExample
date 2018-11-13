using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EventStoreIntegration
{
    public class EventStoreReadProxy : IEventStoreReadProxy
    {
        public async Task<EventFromStream> ReadEventAsync(IEventStoreConnection connection, string stream, long index)
        {
            var result = await connection.ReadEventAsync(stream, index, false);

            if (ResultHasNoEvent(result))
                return new NoEventFound();

            return new LoadedEvent
            {
                Type = result.Event?.Event.EventType,
                Data = result.Event?.Event.Data ?? new byte[] { }
            };
        }

        private static bool ResultHasNoEvent(EventReadResult result)
        {
            return !result.Event.HasValue;
        }
    }
}