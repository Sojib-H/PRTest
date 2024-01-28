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
		public dynamic SignUp(UserInfo userInfo)
		{
			try
			{
				var ApiUrl = "http://localhost:5262/api/Authentication/SignUp";
				return ApiCalling.Post<dynamic>(ApiUrl, userInfo);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost]
		public dynamic Login(UserInfo userInfo)
		{
			try
			{
				var ApiUrl = "http://localhost:5262/api/Authentication/Login";
				return ApiCalling.Post<ReturnParam>(ApiUrl, userInfo);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost]
		public dynamic CheckOTP(OTPInfo OtpInfo)
		{
			try
			{
				var ApiUrl = "http://localhost:5262/api/Authentication/CheckOTP";
				return ApiCalling.Post<ReturnParam>(ApiUrl, OtpInfo);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
