using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Repository.Models.Entity
{
    public partial class LoginParam
	{
        [Key]
        public int UserID { get; set; }
        public string ReturnMsg { get; set; }
    }
}
