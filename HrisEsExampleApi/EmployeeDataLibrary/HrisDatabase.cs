using System.Data.Entity;

namespace EmployeeDataLibrary
{
    public class HrisDatabase : DbContext, IHrisDatabase
    {
        public HrisDatabase()
        : base(@"Server=.\sqlserver2014;Database=HrisExample;Trusted_Connection=True;")
        {
            Database.SetInitializer<HrisDatabase>(null);
        }

        public IDbSet<Employee> Employees { get; set; }
    }
}