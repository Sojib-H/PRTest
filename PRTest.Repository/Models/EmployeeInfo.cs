using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTest.Repository.Models
{
	public class EmployeeInfo
	{
		[Key]
		public int EmpID { get; set; }
		public string EmpName { get; set; }
		public int DepartmentID { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public int CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ModifyDate { get; set; }
	}
}
