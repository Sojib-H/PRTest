using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.UnitOfWork;
using System.Reflection.Emit;
using System.Transactions;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : BaseController
	{
		DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");
		public AuthenticationController(IUnitOfWork uow)
		{
			Uow = uow;
		}

		[HttpPost("[action]")]
		public dynamic Login(UserInfo entity)
		{
			try
			{
				Random generator = new Random();
				var search = Uow.TblUserInfo.FirstOrDefault(x => x.Username == entity.Username && x.Password == entity.Password).Result;
				if (search == null)
				{
					return "Invalid";
				}
				else
				{
					var OtpResult = Uow.TblOTPInfo.FirstOrDefault(x => x.Email == entity.Email).Result;
					OTPInfo OTPData = new OTPInfo()
					{
						Email = OtpResult.Email,
						OTP = generator.Next(100000, 1000000)
					};
					Uow.TblOTPInfo.Update(OTPData);
					return "Success";
				}

			}

			catch (Exception)
			{
				return "Error";
			}
		}

	}
}
