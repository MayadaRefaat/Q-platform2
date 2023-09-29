using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class subjectcoureimg
    {
        [Key]
        public int photoid { get; set; }
        public string photoulr { get; set; }
        [Required]
        public int subjectcoureid { get; set; }

    }
}