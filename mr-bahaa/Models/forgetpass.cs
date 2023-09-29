using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mr_bahaa.Models
{
    public class forgetpass
    {
        [Key]
        public int forgetid { get; set; }
        public string studentname { get; set; }
        public string school { get; set; }
        public string phone { get; set; }
        public string pass { get; set; }
        public string email { get; set; }



    }
}