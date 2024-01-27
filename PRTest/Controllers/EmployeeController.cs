using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.ApiGateway;
using PRTest.Repository.Models;

namespace PRTest.Controllers
{
	public class EmployeeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public dynamic GetEmployee(EmployeeInfo employeeInfo)
        {
            try
            {
                var ApiUrl = "http://localhost:5262/api/Employee/GetEmployee";
                var abc = ApiCalling.PostAll<EmployeeInfo>(ApiUrl, employeeInfo);
                return abc;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
