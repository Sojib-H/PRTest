using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.Models;
using PRTest.Repository.UnitOfWork;
using System.Transactions;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MainController : BaseController
	{
		DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");


		public MainController(IUnitOfWork uow)
		{
			Uow = uow;
		}

		[HttpPost("[action]")]
		public async Task<dynamic> CreateUser(UserInfo entity)
		{
			using (TransactionScope DbRollback = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					var search = Uow.TblUserInfo.FirstOrDefault(x=>x.Email == entity.Email).Result;
					if (search == null)
					{
						UserInfo objUser = new UserInfo()
						{
							UserID = entity.UserID,
							Username = entity.Username,
							Password = entity.Password,
							Email = entity.Email,
							IsActive = entity.IsActive,
							CreateDate = TodayTime,
						};
						await Uow.TblUserInfo.Add(objUser);
						DbRollback.Complete();
						return "Success";
					}
					else
					{
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
	}
}
