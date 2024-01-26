using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Repository.Models.Entity
{
	public partial class UserInfo
	{
		[Key]
		public int UserID { get; set; } = 0;
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public DateTime CreateDate { get; set; }
	}
}
