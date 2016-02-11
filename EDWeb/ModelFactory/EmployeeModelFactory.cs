using System;
using System.Linq;
using EDWeb.Models;
using EDWeb.Repositories;

namespace EDWeb.ModelFactory
{
    public class EmployeeModelFactory
    {
        private readonly AuthRepository _authRepo;

        public EmployeeModelFactory()
        {
            _authRepo = new AuthRepository();
        }

        public EmployeeModel Create(ApplicationUser applicationUser)
        {
            return new EmployeeModel()
            {
                Id = applicationUser.Id,
                FirstName = applicationUser.FirstName,
                UserName = applicationUser.UserName,
                MiddleInitial = applicationUser.MiddleInitial,
                LastName = applicationUser.LastName,
                SecondLastName = applicationUser.SecondLastName,
                JobTitle = applicationUser.JobTitle,
                Location = applicationUser.Location,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber,
                Role = GetRoleName(applicationUser.Id)
            };
        }

        public ApplicationUser Parse(EmployeeModel model)
        {
            try
            {
                var applicationUser = new ApplicationUser
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    UserName = model.UserName,
                    MiddleInitial = model.MiddleInitial,
                    LastName = model.LastName,
                    SecondLastName = model.SecondLastName,
                    JobTitle = model.JobTitle,
                    Location = model.Location,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                return applicationUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetRoleName(string userId)
        {
            return _authRepo.GetRolesByUserId(userId).First();
        }

        public bool SetUserCurrentRole(string userId, string role)
        {
            return _authRepo.ChangeUserCurrentRole(userId, role);
        }
    }
}