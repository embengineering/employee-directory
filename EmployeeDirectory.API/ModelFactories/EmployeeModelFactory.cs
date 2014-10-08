using System;
using EmployeeDirectory.API.Entities;
using EmployeeDirectory.API.Models;

namespace EmployeeDirectory.API.ModelFactories
{
    public class EmployeeModelFactory
    {
        public EmployeeModelFactory()
        {
        }

        public EmployeeModel Create(Employee entity)
        {
            return new EmployeeModel()
            {
                Id = entity.Id,
                FirstName =  entity.FirstName,
                MiddleInitial = entity.MiddleInitial,
                LastName = entity.LastName,
                SecondLastName = entity.SecondLastName,
                JobTitle = entity.JobTitle,
                Location = entity.Location,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber
            };
        }

        public Employee Parse(EmployeeModel model)
        {
            try
            {
                var entity = new Employee
                {
                    Id = model.Id,
                    MiddleInitial = model.MiddleInitial,
                    LastName = model.LastName,
                    SecondLastName = model.SecondLastName,
                    JobTitle = model.JobTitle,
                    Location = model.Location,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}