using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.UnitOfWork;

namespace PRTest.Controllers
{
	public class ReportingController : Controller
	{
		protected IUnitOfWork Uow { get; set; }

		public ReportingController(IUnitOfWork uow)
		{
			Uow = uow;
		}
		DateTime TodayDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");
		public ActionResult NOCReport()
		{
			try
			{
				var strurl = HttpContext.Request.GetDisplayUrl();
				string[] valueurl = strurl.Substring(strurl.LastIndexOf("/") + 1).Split('=');
				var EmpID = valueurl[1];
				ViewBag.Query = Uow.TblEmployeeInfo.FirstOrDefault(x => x.EmpID == Convert.ToInt32(EmpID)).Result;
				//ViewBag.Date = TodayDate.ToString();

				return PartialView();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public ActionResult ReleaseReport()
		{
			try
			{
				var strurl = HttpContext.Request.GetDisplayUrl();
				string[] valueurl = strurl.Substring(strurl.LastIndexOf("/") + 1).Split('=');
				var EmpID = valueurl[1];
				ViewBag.Query = Uow.TblEmployeeInfo.FirstOrDefault(x => x.EmpID == Convert.ToInt32(EmpID)).Result;
				//ViewBag.Date = TodayDate.ToString();

				return PartialView();
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
