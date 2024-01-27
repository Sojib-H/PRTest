using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.Models;
using PRTest.Repository.UnitOfWork;

namespace PRTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");
        public DepartmentController(IUnitOfWork uow)
        {
            Uow = uow;
        }

        [HttpGet("[action]")]
        public IEnumerable<DepartmentInfo> GetAllDepartment()
        {
            try
            {
                return Uow.TblDepartmentInfo.GetAll().Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
