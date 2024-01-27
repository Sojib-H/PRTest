using CRUD.Repository.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRTest.Repository.AesOperation;
using PRTest.Repository.Models;
using PRTest.Repository.UnitOfWork;
using System.Transactions;

namespace PRTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        DateTime TodayTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");
        public EmployeeController(IUnitOfWork uow)
        {
            Uow = uow;
        }

        [HttpPost("[action]")]
        public dynamic GetEmployee(EmployeeInfo entity)
        {
            try
            {
                var EmployeeSearch = Uow.TblEmployeeInfo.FindBy(x => x.CreateBy == entity.CreateBy).Result;

                return EmployeeSearch;
            }

            catch (Exception)
            {
                return "Error";

            }
        }

        [HttpPost("[action]")]
        public dynamic AddEmployee(EmployeeInfo entity)
        {
            using (TransactionScope DbRollback = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var EmployeeSearch = Uow.TblEmployeeInfo.FindBy(x => x.Email == entity.Email && x.Mobile == entity.Mobile).Result;

                    if (EmployeeSearch.Count() > 0)
                    {
                        DbRollback.Dispose();
                        return "Duplicate";
                    }
                    else
                    {
                        entity.CreateDate = TodayTime;
                        entity.ModifyDate = TodayTime;
                        Uow.TblEmployeeInfo.Add(entity);
                        DbRollback.Complete();
                        return "Success";
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
