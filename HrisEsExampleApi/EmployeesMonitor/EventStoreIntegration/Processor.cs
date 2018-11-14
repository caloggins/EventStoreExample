using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using JetBrains.Annotations;

namespace EmployeesMonitor.EventStoreIntegration
{
    [UsedImplicitly]
    public class Processor : IProcessor
    {
        private readonly IConverter converter;
        private readonly IDispatcher dispatcher;

        public Processor(
            IConverter converter,
            IDispatcher dispatcher)
        {
            this.converter = converter;
            this.dispatcher = dispatcher;
        }

        public async Task ProcessEvent(ResolvedEvent evt)
        {
            var type = evt.Event.EventType;
            var data = evt.Event.Data;
            var id = evt.Event.EventStreamId
                .GetIdFromStream();

            Console.WriteLine($"  Processing {type} event for employee {id}.");

            var loaded = converter.ConvertReadToDomain(type, data);

            await dispatcher.Dispatch((dynamic) loaded, id);
        }
    }
}