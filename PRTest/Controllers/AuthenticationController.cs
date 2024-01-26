using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.ApiGateway;

namespace PRTest.Controllers
{
	public class AuthenticationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		public dynamic Login(UserInfo userInfo)
		{
			try
			{
				var ApiUrl = "http://localhost:5262/api/Authentication/Login";
				return ApiCalling.Post<dynamic>(ApiUrl, userInfo);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
