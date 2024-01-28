using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.ApiGateway;
using PRTest.Repository.Models;

namespace PRTest.Controllers
{
	public class CertificateController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CertificateInfo()
		{
			return View();
		}

		[HttpGet]
        public dynamic GetAllCertificateType()
        {
            try
            {
                var ApiUrl = "http://localhost:5262/api/Certificate/GetAllCertificateType";
                return ApiCalling.GetAll<CertificateInfo>(ApiUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

		[HttpPost]
		public dynamic AddEmployeeCertificate(EmployeeCertificateInfo employeeCertificateInfo)
		{
			try
			{
				var ApiUrl = "http://localhost:5262/api/Certificate/AddEmployeeCertificate";
				return ApiCalling.Post<dynamic>(ApiUrl, employeeCertificateInfo);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
