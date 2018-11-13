using System;
using System.Threading.Tasks;

namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public class Reader : IReader
    {
        private readonly IStreamLoader streamLoader;
        private readonly ITypeMapSelector selector;

        public Reader(IStreamLoader streamLoader, ITypeMapSelector selector)
        {
            this.streamLoader = streamLoader;
            this.selector = selector;
        }

        public async Task<TAggregate> Read<TAggregate>(Guid id)
            where TAggregate : Aggregate, new()
        {
            var aggregate = new TAggregate();
            var stream = $"{aggregate.StreamName}.{id}";

            var events = await streamLoader.LoadEvents(stream, selector.For(typeof(TAggregate)));

            aggregate.LoadHistory(events);

            return aggregate;
        }
    }
}