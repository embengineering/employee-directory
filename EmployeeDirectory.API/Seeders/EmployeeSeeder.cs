using System;
using System.Linq;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Entities;

namespace EmployeeDirectory.API.Seeders
{

    public class EmployeeSeeder
    {
        readonly AppContext _ctx;
        public EmployeeSeeder(AppContext ctx)
        {
            _ctx = ctx;
        }

        public void Seed()
        {
            try
            {
                // add at least one employee
                //_ctx.Employees.Add(new Employee
                //{
                    
                //});

                //_ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
