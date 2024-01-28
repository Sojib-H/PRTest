using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.AesOperation;
using PRTest.Repository.UnitOfWork;
using System.Reflection.Emit;
using System.Transactions;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : BaseController
	{
		[Obsolete]
		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _IHostingEnvironment;
		DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");

		[Obsolete]
		public AuthenticationController(IUnitOfWork uow, Microsoft.AspNetCore.Hosting.IHostingEnvironment iHostingEnvironment)
		{
			Uow = uow;
			_IHostingEnvironment = iHostingEnvironment;
		}

		[HttpPost("[action]")]
		public dynamic SignUp(UserInfo entity)
		{
			using (TransactionScope DbRollback = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					var key = "kjkla7ksf343l34knlasefal34j5k145";
					var search = Uow.TblUserInfo.FirstOrDefault(x => x.Username == entity.Username && x.Email == entity.Email).Result;
					if (search == null)
					{
						entity.CreateDate = TodayTime;
						entity.Password = AesOperation.EncryptString(key, entity.Password);
						Uow.TblUserInfo.Add(entity);
						DbRollback.Complete();
						return "Success";
					}
					else
					{
						DbRollback.Dispose();
						return "Duplicate";
					}

				}

				catch (Exception)
				{
					DbRollback.Dispose();
					return "Error";
				}
			}
		}

		[Obsolete]
		[HttpPost("[action]")]
		public async Task<dynamic> Login(UserInfo entity)
		{
			using (TransactionScope DbRollback = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					MailController MailObj = new MailController();
					var MailBodyPath = "";
					var key = "kjkla7ksf343l34knlasefal34j5k145";
					entity.Password = AesOperation.EncryptString(key, entity.Password);
					Random generator = new Random();
					var search = Uow.TblUserInfo.FirstOrDefault(x => x.Username == entity.Username && x.Password == entity.Password).Result;
					if (search == null)
					{
						return new ReturnParam()
						{
							ReturnCode = 0,
							ReturnMsg = "Invalid",
						};
					}
					else
					{
						OTPInfo OTPData = new OTPInfo();
						var OtpResult = Uow.TblOTPInfo.FirstOrDefault(x => x.Email == search.Email).Result;
						if (OtpResult == null)
						{

							OTPData.Email = search.Email;
							OTPData.OTP = generator.Next(100000, 1000000);
							await Uow.TblOTPInfo.Add(OTPData);
						}
						else
						{
							OTPData.OTPID = OtpResult.OTPID;
							OTPData.Email = search.Email;
							OTPData.OTP = generator.Next(100000, 1000000);
							await Uow.TblOTPInfo.Update(OTPData);
						}

						MailBodyPath = Path.Combine(_IHostingEnvironment.ContentRootPath, "MailTemplate/MailBody.html");
						var mailReturn = MailObj.SendEmail(OtpResult.Email, MailBodyPath, OTPData.OTP);

						DbRollback.Complete();

						return new ReturnParam()
						{
							ReturnCode = OTPData.OTP,
							ReturnMsg = "Success",
							Email = search.Email
						};
					}

				}

				catch (Exception)
				{
					DbRollback.Dispose();
					return new ReturnParam()
					{
						ReturnCode = 0,
						ReturnMsg = "Error",
					};
				}
			}
		}

		[HttpPost("[action]")]
		public dynamic CheckOTP(OTPInfo entity)
		{
			try
			{
				var OtpSearch = Uow.TblOTPInfo.FirstOrDefault(x => x.Email == entity.Email && x.OTP == entity.OTP).Result;
				if (OtpSearch == null)
				{
					return new ReturnParam()
					{
						ReturnCode = 0,
						ReturnMsg = "Invalid",
					};
				}
				else
				{
					var UserSearch = Uow.TblUserInfo.FirstOrDefault(x => x.Email == OtpSearch.Email).Result;

					return new ReturnParam()
					{
						ReturnCode = UserSearch.UserID,
						ReturnMsg = "Success",
					};
				}

			}

			catch (Exception)
			{
				return new ReturnParam()
				{
					ReturnCode = 0,
					ReturnMsg = "Error",
				};

			}
		}

	}
}
