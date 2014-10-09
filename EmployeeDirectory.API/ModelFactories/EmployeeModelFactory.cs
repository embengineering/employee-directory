using System;
using EmployeeDirectory.API.Models;

namespace EmployeeDirectory.API.ModelFactories
{
    public class EmployeeModelFactory
    {
        public EmployeeModel Create(ApplicationUser entity)
        {
            return new EmployeeModel()
            {
                UserName = entity.UserName,
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

        public ApplicationUser Parse(EmployeeModel model)
        {
            try
            {
                var entity = new ApplicationUser
                {
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