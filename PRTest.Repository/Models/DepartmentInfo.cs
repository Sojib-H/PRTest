using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTest.Repository.Models
{
	public class DepartmentInfo
	{
		[Key]
		public int DepartmentID { get; set; }
		public string DepartmentName { get; set; }
	}
}
