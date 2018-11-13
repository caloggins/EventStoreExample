using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeBenefitsLibrary;

namespace EventStoreIntegration
{
    public interface IStreamLoader
    {
        Task<IEnumerable<Event>> LoadEvents(string stream, TypeMap types);
    }
}