using System.Data.Entity;

namespace EmployeeDataLibrary
{
    public interface IHrisDatabase
    {
        IDbSet<Employee> Employees { get; set; }
    }
}