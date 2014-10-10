using System;
using System.Data.Entity;
using System.Linq;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmployeeDirectory.API.Repositories
{
    public class EmployeeRepository : IDisposable
    {
        private readonly AppContext _ctx;

        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeRepository()
        {
            _ctx = new AppContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public IQueryable<ApplicationUser> Get()
        {
            return _ctx.Users.AsQueryable().Include("Roles");
        }

        public ApplicationUser GetEmployee(string id)
        {
            return _ctx.Users.Include("Roles").FirstOrDefault(x => x.Id == id);
        }

        public bool InsertEmployee(ApplicationUser entity)
        {
            try
            {
                _ctx.Users.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEmployee(ApplicationUser originalEmployee, EmployeeModel updatedEmployee)
        {
            _ctx.Entry(originalEmployee).CurrentValues.SetValues(updatedEmployee);
            return true;
        }

        public bool DeleteEmployee(string id)
        {
            try
            {
                var entity = _ctx.Users.FirstOrDefault(x => x.Id == id);
                if (entity != null)
                {
                    _ctx.Users.Remove(entity);
                    return true;
                }
            }
            catch
            {
                // TODO Logging
            }

            return false;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}