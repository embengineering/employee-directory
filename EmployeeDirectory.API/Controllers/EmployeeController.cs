using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeDirectory.API.ModelFactories;
using EmployeeDirectory.API.Repositories;

namespace EmployeeDirectory.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeRepository _repo;
        private readonly EmployeeModelFactory _modelFactory;

        public EmployeeController()
        {
            _repo = new EmployeeRepository();
            _modelFactory = new EmployeeModelFactory();
        }

        // GET api/employee/all
        [Authorize]
        [Route("all")]
        public IHttpActionResult Get()
        {
            var query = _repo.GetAll();

            var results = query
                .ToList().OrderByDescending(s => s.Id)
                .Select(s => _modelFactory.Create(s));

            return Ok(results);
        }
    }
}
