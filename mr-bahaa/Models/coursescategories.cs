using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class coursescategories
    {
        [Key]
        public int catigoriesid { get; set; }
        [Required]
        public string name { get; set; }


    }
}