using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public interface IStreamLoader
    {
        Task<IEnumerable<Event>> LoadEvents(string stream, TypeMap types);
    }
}