using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EDWeb.Contexts;
using EDWeb.Models;

namespace EDWeb.Repositories
{
    public class AuthRepository : IDisposable
    {
        private readonly AppContext _ctx;

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AppContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var userStore = new UserStore<ApplicationUser>(_ctx);
            var userManager = new ApplicationUserManager(userStore);

            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                MiddleInitial = userModel.MiddleInitial,
                LastName = userModel.LastName,
                SecondLastName = userModel.SecondLastName,
                JobTitle = userModel.JobTitle,
                Location = userModel.Location,
                PhoneNumber = userModel.PhoneNumber
            };

            // create user (employee)
            var result = await _userManager.CreateAsync(user, userModel.Password);

            // add role to user
            await userManager.AddToRoleAsync(user.Id, userModel.Role);

            return result;
        }

        public bool ChangeUserCurrentRole(string userId, string role)
        {
            var userStore = new UserStore<ApplicationUser>(_ctx);
            var userManager = new ApplicationUserManager(userStore);
            var roleStore = new RoleStore<ApplicationRole>(_ctx);

            // get array of all roles
            var roles = roleStore.Roles.Select(s => s.Name).ToArray();

            // remove all roles from user
            userManager.RemoveFromRoles(userId, roles);

            // assign new role to user
            var result = userManager.AddToRole(userId, role);

            return result.Succeeded;
        }


        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public IList<string> GetRolesByUserId(string userId)
        {
            return _userManager.GetRoles(userId);
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}