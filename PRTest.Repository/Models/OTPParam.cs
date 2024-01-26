using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Repository.Models.Entity
{
    public partial class OTPParam
    {
        [Key]
        public int OTP { get; set; }
        public string ReturnMsg { get; set; }
    }
}
