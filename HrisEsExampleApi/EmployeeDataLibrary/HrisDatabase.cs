using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataLibrary
{
    public class HrisDatabase : DbContext, IHrisDatabase
    {
        [UsedImplicitly]
        public HrisDatabase()
        {

        }

        // this is a test seam.
        public HrisDatabase(DbContextOptions<HrisDatabase> options)
        : base(options)
        {

        }

        [UsedImplicitly]
        public DbSet<Employee> Employees { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=.\sqlserver2014;Database=HrisExample;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("Employee")
                .HasKey(o => o.EmployeeKey);
        }
    }
}