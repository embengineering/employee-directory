using System.Collections.Generic;
using System.Web.Http;

namespace EmployeeDirectory.API.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        [Authorize]
        [Route("All")]
        public IHttpActionResult Get()
        {
            return Ok(Order.CreateEmployees());
        }
    }

    #region Helpers

    public class Order
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Picture { get; set; }

        public static List<Order> CreateEmployees()
        {
            var orderList = new List<Order> 
            {
                new Order {EmployeeId = 10248, EmployeeName = "Taiseer Joudeh", JobTitle = "Amman" },
                new Order {EmployeeId = 10249, EmployeeName = "Ahmad Hasan", JobTitle = "Dubai" },
                new Order {EmployeeId = 10250,EmployeeName = "Tamer Yaser", JobTitle = "Jeddah"},
                new Order {EmployeeId = 10251,EmployeeName = "Lina Majed", JobTitle = "Abu Dhabi" },
                new Order {EmployeeId = 10252,EmployeeName = "Yasmeen Rami", JobTitle = "Kuwait" }
            };

            return orderList;
        }
    }

    #endregion
}
