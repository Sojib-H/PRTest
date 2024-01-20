using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Repository.Models.Entity
{
    public partial class UserType
    {
        [Key]
        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
    }
}
