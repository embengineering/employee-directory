using System.Data.Entity;
using EmployeeDirectory.API.ContextMigrations;
using EmployeeDirectory.API.Entities;
using EmployeeDirectory.API.Mappers;

namespace EmployeeDirectory.API.Contexts
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("AppContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            // Enable to prepopulate database with dummy data
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext, AppContextMigrationConfiguration>());
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
