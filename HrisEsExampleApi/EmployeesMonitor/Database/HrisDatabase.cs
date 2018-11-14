using System.Data.Entity;
using JetBrains.Annotations;

namespace EmployeesMonitor.Database
{
    [UsedImplicitly]
    public class HrisDatabase : DbContext, IHrisDatabase
    {
        public HrisDatabase()
        :base(@"Server=.\sqlserver2014;Database=HrisExample;Trusted_Connection=True;")
        {
            System.Data.Entity.Database.SetInitializer<HrisDatabase>(null);
        }

        public IDbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("Employee")
                .HasKey(o => o.EmployeeKey);
        }
    }
}