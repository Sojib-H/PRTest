using CRUD.Repository.Models.Entity;
using Microsoft.EntityFrameworkCore;
using PRTest.Repository.Models;

namespace PRTest.Repository.DBContext
{
	public partial class PRContext : DbContext
	{
        public PRContext(){}
		public PRContext(DbContextOptions options) : base(options) { }

		public DbSet<CertificateInfo> CertificateInfo { get; set; }
		public DbSet<DepartmentInfo> DepartmentInfo { get; set; }
		public DbSet<EmployeeInfo> EmployeeInfo { get; set; }
		public DbSet<Role> Role { get; set; }
		public DbSet<RolePermission> RolePermission { get; set; }
		public DbSet<UserInfo> UserInfo { get; set; }
		public DbSet<UserType> UserType { get; set; }
		public DbSet<OTPInfo> OTPInfo { get; set; }
		public DbSet<EmployeeCertificateInfo> EmployeeCertificateInfo { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}
