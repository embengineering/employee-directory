using System.Data.Entity.Migrations;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Seeders;

namespace EmployeeDirectory.API.ContextMigrations
{
    public class AppContextMigrationConfiguration : DbMigrationsConfiguration<AppContext>
    {
        public AppContextMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppContext context)
        {
            new RolesSeeder(context).Seed();
        }

    }
}
