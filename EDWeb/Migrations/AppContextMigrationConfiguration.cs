using System.Data.Entity.Migrations;
using EDWeb.Contexts;
using EDWeb.Seeders;

namespace EDWeb.Migrations
{
    internal sealed class AppContextMigrationConfiguration : DbMigrationsConfiguration<AppContext>
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
