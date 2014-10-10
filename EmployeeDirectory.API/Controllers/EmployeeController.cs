using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using EmployeeDirectory.API.Models;
using EmployeeDirectory.API.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmployeeDirectory.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeRepository _employeeRepo;
        private readonly AuthRepository _authRepo;

        public EmployeeController()
        {
            _employeeRepo = new EmployeeRepository();
            _authRepo = new AuthRepository();
        }

        // GET api/employees
        [Authorize]
        [Route("")]
        public PageResult<EmployeeModel> GetEmployees(ODataQueryOptions options)
        {
            var query = _employeeRepo.Get();

            // create query
            var filteredQuery = options.ApplyTo(query.Select(s => new EmployeeModel()
            {
                Id = s.Id,
                UserName = s.UserName,
                FirstName = s.FirstName,
                MiddleInitial = s.MiddleInitial,
                LastName = s.LastName,
                SecondLastName = s.SecondLastName,
                JobTitle = s.JobTitle,
                Location = s.Location,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Role = ""
            })) as IQueryable<EmployeeModel>;

            // convert query to list to allow manipulation
            var employees = filteredQuery != null ? filteredQuery.ToList() : new List<EmployeeModel>();

            // make sure to include the role description
            foreach (var employee in employees)
            {
                employee.Role = _authRepo.GetRolesByUserId(employee.Id).First();
            }

            long? count = query.Count();

            return new PageResult<EmployeeModel>(employees, null, count);
        }

        [Authorize]
        [Route("{id}")]
        public IHttpActionResult GetEmployee(string id)
        {
            // find employee
            var employee = _employeeRepo.GetEmployee(id);

            // get user roles (take first one as default)
            var defaultUserRole = _authRepo.GetRolesByUserId(id).First();

            // check if employee was found
            if (employee == null) return NotFound();

            // get employee roles
            var userRole = employee.Roles.FirstOrDefault();

            if (userRole != null)
            {
                return Ok(new EmployeeModel
                {
                    Id = employee.Id,
                    UserName = employee.UserName,
                    FirstName = employee.FirstName,
                    MiddleInitial = employee.MiddleInitial,
                    LastName = employee.LastName,
                    SecondLastName = employee.SecondLastName,
                    JobTitle = employee.JobTitle,
                    Location = employee.Location,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Role = defaultUserRole
                });
            }

            return NotFound();
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteEmployee(string id)
        {
            // remove employeee
            bool deleted = _employeeRepo.DeleteEmployee(id);

            if (deleted)
            {
                _employeeRepo.SaveAll();
               return Ok();
            }

            return NotFound();
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateEmployee([FromUri]string id, [FromBody]EmployeeModel updatedEmployee)
        {
            // find employee
            var employee = _employeeRepo.GetEmployee(id);
            var updated = _employeeRepo.UpdateEmployee(employee, updatedEmployee);

            // update role
            // update role if changed
            var originalEmployeeRole = _authRepo.GetRolesByUserId(id).First();

            // compare roles
            if (originalEmployeeRole != updatedEmployee.Role)
            {
                // add role to user
                _authRepo.ChangeUserCurrentRole(id, updatedEmployee.Role);
            }

            if (updated)
            {
                _employeeRepo.SaveAll();
                _authRepo.SaveAll();
                return Ok();
            }

            return BadRequest();
        }
    }
}
