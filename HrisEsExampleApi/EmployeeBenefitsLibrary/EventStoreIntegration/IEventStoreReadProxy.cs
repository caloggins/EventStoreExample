using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public interface IEventStoreReadProxy
    {
        Task<EventFromStream> ReadEventAsync(IEventStoreConnection connection, string stream, long index);
    }
}