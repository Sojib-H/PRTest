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
    }
}
