using System.Linq;
using System.Text;
using EmployeeBenefitsLibrary;
using Newtonsoft.Json;

namespace EventStoreIntegration
{
    public class StreamToDomainEventConverter : IConverter
    {
        public Event ConvertReadToDomain(TypeMap types, LoadedEvent eventFromStream)
        {
            var json = Encoding.UTF8.GetString(eventFromStream.Data);
            var type = types.Single(o => o.name == eventFromStream.Type).type;
            var evt = JsonConvert.DeserializeObject(json, type)
                as Event;
            return evt;
        }
    }
}