using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class course
    {
        [Key]
        public int courseid { get; set; }
        public string day { get; set; }
        public string time { get; set; }
    }
}