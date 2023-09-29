using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class photolec
    {
        [Key]
        public int photoid { get; set; }
        public string photoUrl { get; set; }
        public int lecid { get; set; }
        public string type { get; set; }

    }
}