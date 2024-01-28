using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Repository.Models.Entity
{
    public partial class ReturnParam
    {
        [Key]
        public int ReturnCode { get; set; }
        public string ReturnMsg { get; set; }
        public string Email { get; set; }
    }
}
