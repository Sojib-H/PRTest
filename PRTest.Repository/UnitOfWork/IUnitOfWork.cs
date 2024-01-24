using CRUD.Repository.Models.Entity;
using PRTest.Repository.Models;
using PRTest.Repository.Repository;

namespace PRTest.Repository.UnitOfWork
{
	public interface IUnitOfWork
	{
		IRepository<CertificateInfo> TblCertificateInfo { get; }
		IRepository<DepartmentInfo> TblDepartmentInfo { get; }
		IRepository<EmployeeInfo> TblEmployeeInfo { get; }
		IRepository<UserInfo> TblUserInfo { get; }
		IRepository<OTPInfo> TblOTPInfo { get; }
		
	}
}
