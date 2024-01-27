using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.Models;
using PRTest.Repository.UnitOfWork;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : BaseController
	{
		DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");
		public EmployeeController(IUnitOfWork uow)
		{
			Uow = uow;
		}

		[HttpPost("[action]")]
		public dynamic GetEmployee(EmployeeInfo entity)
		{
			try
			{
				var EmployeeSearch = Uow.TblEmployeeInfo.FindBy(x => x.CreateBy == entity.CreateBy).Result;

				return EmployeeSearch;
			}

			catch (Exception)
			{
				return "Error";

			}
		}
	}
}
