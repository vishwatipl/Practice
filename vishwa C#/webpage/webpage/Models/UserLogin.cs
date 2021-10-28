using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace webpage.Models
{
    public class UserLogin
    {  
        [Display(Name ="EmailId")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "EmailID is required")]
        public string EmailID { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool Rememberme { get; set; }
    }
}