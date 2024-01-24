using Microsoft.AspNetCore.Mvc;

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
	}
}
