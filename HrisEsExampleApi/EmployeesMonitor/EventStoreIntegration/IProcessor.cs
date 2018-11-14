using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EmployeesMonitor.EventStoreIntegration
{
    public interface IProcessor
    {
        Task ProcessEvent(ResolvedEvent evt);
    }
}