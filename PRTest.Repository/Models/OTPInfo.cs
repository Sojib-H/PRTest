using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Repository.Models.Entity
{
    public partial class OTPInfo
	{
        [Key]
        public int OTPID { get; set; }
        public string Email { get; set; }
        public int OTP { get; set; }
    }
}
