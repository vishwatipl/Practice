﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcEmployeeModel
    {
        public int e_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone_no { get; set; }
        public string position { get; set; }
        public Nullable<int> age { get; set; }
        public Nullable<int> salary { get; set; }
    }
}