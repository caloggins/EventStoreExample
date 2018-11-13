using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataLibrary
{
    public interface IHrisDatabase
    {
        DbSet<Employee> Employees { get; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}