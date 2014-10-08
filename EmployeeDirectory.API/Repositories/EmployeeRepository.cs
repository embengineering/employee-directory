using System;
using System.Linq;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Entities;

namespace EmployeeDirectory.API.Repositories
{
    public class EmployeeRepository : IDisposable
    {
        private readonly AppContext _ctx;

        public EmployeeRepository()
        {
            _ctx = new AppContext();
        }

        public IQueryable<Employee> GetAll()
        {
            return _ctx.Employees.AsQueryable();
        }

        public Employee GetEmployee(int id)
        {
            return _ctx.Employees.Find(id);
        }

        public bool InsertEmployee(Employee entity)
        {
            try
            {
                _ctx.Employees.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEmployee(Employee originalEntity, Employee updatedEntity)
        {
            _ctx.Entry(originalEntity).CurrentValues.SetValues(updatedEntity);
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var entity = _ctx.Employees.Find(id);
                if (entity != null)
                {
                    _ctx.Employees.Remove(entity);
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