using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Repository.Models.Entity
{
    public partial class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int CompanyID { get; set; }
        public bool IsActive { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
