using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace webpage.Models
{
    public class ResetPasswordModel
    { 
        [Required(ErrorMessage ="New Password required",AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="New Password and Confirm password dose not match")]
        public string CPassword { get; set; }
        [Required]
        public string ResetCode { get; set; }

    }
}