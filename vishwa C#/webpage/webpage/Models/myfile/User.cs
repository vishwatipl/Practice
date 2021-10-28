using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace webpage.Models
{
    [MetadataType(typeof(Usremetadata))]
    public partial class user
    {

    }
    public class Usremetadata
    {
        [Display(Name = "FirstName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "firstname is required")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        [Display(Name = "EmailId")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "emailId is required")]
        public string EmailID { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "(0:MM/dd/yyyy)")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.DateTime)]
        [MinLength(6, ErrorMessage = "minimum 6 char required")]
        public string Password { get; set; }

        //[Display(Name = "IsEmailVerified")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "IsEmailVerified is required")]
        //public Nullable<bool> IsEmailVerified { get; set; }

        //[Display(Name = "ActivationCode")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "ActivationCode is required")]
        //public Nullable<System.Guid> ActivationCode { get; set; }

        [Display(Name = "Mobile")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }

        [Display(Name = "ResetPasswordCode ")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "ResetPasswordCode not match ")]
        public string ResetPasswordCode { get; set; }


    }


}