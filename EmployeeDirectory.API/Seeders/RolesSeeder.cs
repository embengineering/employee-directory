using System.Web;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EmployeeDirectory.API.Seeders
{

    public class RolesSeeder
    {
        readonly AppContext _ctx;
        public RolesSeeder(AppContext ctx)
        {
            _ctx = ctx;
        }

        public void Seed()
        {
            var roleStore = new RoleStore<ApplicationRole>(_ctx);
            var userStore = new UserStore<ApplicationUser>(_ctx);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // define default system administrator user if doesn't exist
            var user = userManager.FindByEmail("admin@example.com");
            if (user == null)
            {
                // create default/initial system roles
                roleManager.Create(new ApplicationRole { Name = "SYSADMIN", Description = "System Administrator" });
                roleManager.Create(new ApplicationRole { Name = "HR", Description = "Human Resource" });
                roleManager.Create(new ApplicationRole { Name = "EMPLOYEE", Description = "Employee" });

                // sysadmin user
                user = new ApplicationUser
                {
                    UserName = "sysadmin@demo.com",
                    FirstName = "System",
                    LastName = "Administrator",
                    Email = "admin@example.com"
                };
                userManager.Create(user, "welcome");
                userManager.AddToRole(user.Id, "SYSADMIN");

                // hr user
                user = new ApplicationUser
                {
                    UserName = "hr@demo.com",
                    FirstName = "HR",
                    LastName = "User",
                    Email = "hr@demo.com"
                };
                userManager.Create(user, "welcome");
                userManager.AddToRole(user.Id, "HR");
            }
        }
    }
}
