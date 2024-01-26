using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.UnitOfWork;
using System.Transactions;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OTPController : BaseController
	{
		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _IHostingEnvironment;
		DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");


		public OTPController(IUnitOfWork uow, Microsoft.AspNetCore.Hosting.IHostingEnvironment iHostingEnvironment)
		{
			Uow = uow;
			_IHostingEnvironment = iHostingEnvironment;
		}

		[HttpGet("[action]")]
		public dynamic GetAllOTP()
		{
			try
			{
				return Uow.TblOTPInfo.GetAll().Result;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[HttpGet("[action]/{email}")]
		public async Task<dynamic> GenerateOTP(string email)
		{
			using (TransactionScope DbRollback = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					MailController MailObj = new MailController();
					var MailBodyPath = "";
					Random generator = new Random();
					var result = Uow.TblOTPInfo.FirstOrDefault(x => x.Email == email).Result;
					OTPInfo OTPData = new OTPInfo()
					{
						Email = email,
						OTP = generator.Next(100000, 1000000)
					};
					if (result == null)
					{
						await Uow.TblOTPInfo.Add(OTPData);
					}
					else
					{
						OTPData.OTPID = result.OTPID;
						await Uow.TblOTPInfo.Update(OTPData);
					}

					//MailBodyPath = Path.Combine(_IHostingEnvironment.ContentRootPath, "MailTemplate/MailBody.html");
					//var mailReturn = MailObj.SendEmail(email, MailBodyPath, OTPData.OTP);

					DbRollback.Complete();
					return new OTPParam()
					{
						OTP = OTPData.OTP,
						ReturnMsg = "Success",
					};
				}

				catch (Exception)
				{
					DbRollback.Dispose();
					return new OTPParam()
					{
						OTP = 0,
						ReturnMsg = "Error",
					};
				}
			}
		}
	}
}
