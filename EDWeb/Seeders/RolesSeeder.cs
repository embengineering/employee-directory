using System.Web;
using EDWeb.Contexts;
using EDWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EDWeb.Seeders
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
            var user = userManager.FindByEmail("hr@demo.com");
            if (user == null)
            {
                // create default/initial system roles
                roleManager.Create(new ApplicationRole { Name = "HR", Description = "Human Resource" });
                roleManager.Create(new ApplicationRole { Name = "EMPLOYEE", Description = "Employee" });

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
