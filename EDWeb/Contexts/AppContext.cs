using System.Data.Entity;
using EDWeb.Migrations;
using EDWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EDWeb.Contexts
{
    public class AppContext : IdentityDbContext<ApplicationUser>
    {
        public AppContext() : base("AppContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            // enable to prepopulate database with dummy/initial data
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext, AppContextMigrationConfiguration>());
        }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}
