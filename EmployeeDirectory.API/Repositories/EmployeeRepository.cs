using System;
using System.Linq;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Models;

namespace EmployeeDirectory.API.Repositories
{
    public class EmployeeRepository : IDisposable
    {
        private readonly AppContext _ctx;

        public EmployeeRepository()
        {
            _ctx = new AppContext();
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _ctx.Users.AsQueryable().Take(500);
        }

        public ApplicationUser GetEmployee(int id)
        {
            return _ctx.Users.Find(id);
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

        public bool UpdateEmployee(ApplicationUser originalEntity, ApplicationUser updatedEntity)
        {
            _ctx.Entry(originalEntity).CurrentValues.SetValues(updatedEntity);
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var entity = _ctx.Users.Find(id);
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