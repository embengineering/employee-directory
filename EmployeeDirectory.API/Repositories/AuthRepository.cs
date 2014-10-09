using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Models;

namespace EmployeeDirectory.API.Repositories
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
            var user = new ApplicationUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}