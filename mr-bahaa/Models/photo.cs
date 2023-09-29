using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class photo
    {
        [Key]
        public int photoid { get; set; }
        public string photoUrl { get; set; }
        public string userid { get; set; }
    }
}