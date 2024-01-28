using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.AesOperation;
using PRTest.Repository.Models;
using PRTest.Repository.UnitOfWork;
using System.Transactions;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CertificateController : BaseController
	{
		[Obsolete]
		private readonly IHostingEnvironment _IHostingEnvironment;
		DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");

		[Obsolete]
		public CertificateController(IUnitOfWork uow, IHostingEnvironment iHostingEnvironment)
		{
			Uow = uow;
			_IHostingEnvironment = iHostingEnvironment;
		}

		[HttpGet("[action]")]
		public IEnumerable<CertificateInfo> GetAllCertificateType()
		{
			try
			{
				return Uow.TblCertificateInfo.GetAll().Result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPost("[action]")]
		[Obsolete]
		public dynamic AddEmployeeCertificate(EmployeeCertificateInfo entity)
		{
			using (TransactionScope DbRollback = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					var FileName = "";
					if (entity.Certificate != null)
					{
						FileName = Convert.ToString(Guid.NewGuid()).Trim() + ".pdf";
						var path = Path.Combine(_IHostingEnvironment.ContentRootPath, "CertificateFile/", FileName);
						System.IO.File.WriteAllBytes(path, Convert.FromBase64String(entity.Certificate));
					}


					entity.Certificate = FileName;
					entity.CreateDate = TodayTime;
					Uow.TblEmployeeCertificateInfo.Add(entity);
					DbRollback.Complete();
					return "Success";
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
