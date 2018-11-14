using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeesMonitor.Database
{
    public interface IHrisDatabase
    {
        IDbSet<Employee> Employees { get; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}