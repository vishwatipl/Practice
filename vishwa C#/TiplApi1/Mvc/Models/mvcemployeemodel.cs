using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace Mvc.Models
{
    public class mvcemployeemodel
    {
        public int e_id { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string phone_no { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string position { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public Nullable<int> age { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public Nullable<int> salary { get; set; }
    }
}