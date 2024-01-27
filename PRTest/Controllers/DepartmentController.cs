using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.ApiGateway;
using PRTest.Repository.Models;

namespace PRTest.Controllers
{
	public class DepartmentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public dynamic GetAllDepartment() {
            try
            {
                var ApiUrl = "http://localhost:5262/api/Department/GetAllDepartment";
                return ApiCalling.GetAll<DepartmentInfo>(ApiUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}
