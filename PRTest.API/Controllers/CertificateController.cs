using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.Models;
using PRTest.Repository.UnitOfWork;

namespace PRTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : BaseController
    {
        DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");
        public CertificateController(IUnitOfWork uow)
        {
            Uow = uow;
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
    }
}
