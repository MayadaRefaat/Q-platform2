using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class control
    {
        [Key]
        public int id { get; set; }
        public bool openregister { get; set; }
    }
}