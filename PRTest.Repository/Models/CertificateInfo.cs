using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTest.Repository.Models
{
	public class CertificateInfo
	{
		[Key]
		public int CertificateID { get; set; }
		public string CertificateType { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
