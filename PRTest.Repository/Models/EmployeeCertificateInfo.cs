using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTest.Repository.Models
{
	public class EmployeeCertificateInfo
	{
		[Key]
		public int ID { get; set; }
		public int EmpID { get; set; }
		public string Certificate { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
