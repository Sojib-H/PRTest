using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.ApiGateway;

namespace PRTest.Controllers
{
	public class MVCMainController : Controller
	{
		[HttpPost]
		public dynamic CreateUser(UserInfo userInfo)
		{
			try
			{
				var ApiUrl = "http://localhost:5262/api/Main/CreateUser";
				return ApiCalling.Post<dynamic>(ApiUrl, userInfo);
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
