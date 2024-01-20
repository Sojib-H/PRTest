using CRUD.Repository.Models.Entity;
using PRTest.Repository.DBContext;
using PRTest.Repository.Models;
using PRTest.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTest.Repository.UnitOfWork
{
	public class UnitOfWork:IUnitOfWork
	{
		private PRContext _context { get; set; }
		public UnitOfWork(PRContext context)
		{
			this._context = context;
		}

		private IRepository<CertificateInfo> tblCertificateInfo;
		public IRepository<CertificateInfo> TblCertificateInfo
		{
			get
			{
				if (this.tblCertificateInfo == null)
				{
					this.tblCertificateInfo = new GenericRepository<CertificateInfo>(_context);
				}
				return tblCertificateInfo;
			}
		}

		private IRepository<DepartmentInfo> tblDepartmentInfo;
		public IRepository<DepartmentInfo> TblDepartmentInfo
		{
			get
			{
				if (this.tblDepartmentInfo == null)
				{
					this.tblDepartmentInfo = new GenericRepository<DepartmentInfo>(_context);
				}
				return tblDepartmentInfo;
			}
		}

		private IRepository<EmployeeInfo> tblEmployeeInfo;
		public IRepository<EmployeeInfo> TblEmployeeInfo
		{
			get
			{
				if (this.tblEmployeeInfo == null)
				{
					this.tblEmployeeInfo = new GenericRepository<EmployeeInfo>(_context);
				}
				return tblEmployeeInfo;
			}
		}

		private IRepository<UserInfo> tblUserInfo;
		public IRepository<UserInfo> TblUserInfo
		{
			get
			{
				if (this.tblUserInfo == null)
				{
					this.tblUserInfo = new GenericRepository<UserInfo>(_context);
				}
				return tblUserInfo;
			}
		}
	}
}
