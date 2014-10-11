using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using EmployeeDirectory.API.ModelFactory;
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
        private readonly EmployeeModelFactory _employeeModelFactory;

        public EmployeeController()
        {
            _employeeRepo = new EmployeeRepository();
            _employeeModelFactory = new EmployeeModelFactory();
        }

        // GET api/employees
        [Authorize]
        [Route("")]
        public PageResult<EmployeeModel> GetEmployees(ODataQueryOptions options)
        {
            var query = _employeeRepo.Get();
            long? count = 0;

            // set total quantity
            count = options.Filter != null
                ? options.Filter.ApplyTo(query.Select(s => new EmployeeModel()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    UserName = s.UserName,
                    MiddleInitial = s.MiddleInitial,
                    LastName = s.LastName,
                    SecondLastName = s.SecondLastName,
                    JobTitle = s.JobTitle,
                    Location = s.Location,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Role = ""
                }), new ODataQuerySettings()).Count() 
                : query.Count();

            // create query
            var filteredQuery = options.ApplyTo(query.Select(s => new EmployeeModel()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                UserName = s.UserName,
                MiddleInitial = s.MiddleInitial,
                LastName = s.LastName,
                SecondLastName = s.SecondLastName,
                JobTitle = s.JobTitle,
                Location = s.Location,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Role = ""
            }), new ODataQuerySettings()) as IQueryable<EmployeeModel>;

            // convert query to list to allow manipulation
            var employees = filteredQuery != null ? filteredQuery.ToList() : new List<EmployeeModel>();

            // make sure to include the role description
            foreach (var employee in employees)
            {
                employee.Role = _employeeModelFactory.GetRoleName(employee.Id);
            }

            return new PageResult<EmployeeModel>(employees, null, count);
        }

        [Authorize]
        [Route("{id}")]
        public IHttpActionResult GetEmployee(string id)
        {
            // find employee
            var employee = _employeeRepo.GetEmployee(id);

            // check if employee was found
            if (employee == null) return NotFound();

            // get employee roles
            var userRole = employee.Roles.FirstOrDefault();

            if (userRole != null)
            {
                // create employee model
                var employeeModel = _employeeModelFactory.Create(employee);

                return Ok(employeeModel);
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
            var originalEmployeeRole = _employeeModelFactory.GetRoleName(id);

            // compare roles
            if (originalEmployeeRole != updatedEmployee.Role)
            {
                // add role to user
                _employeeModelFactory.SetUserCurrentRole(id, updatedEmployee.Role);
            }

            if (updated)
            {
                _employeeRepo.SaveAll();
                return Ok();
            }

            return BadRequest();
        }
    }
}
