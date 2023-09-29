using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class subjectcoure
    {
        [Key]
        public int subjectcourseid { get; set; }
        [Required]
        public string name { get; set; }
        public virtual Teacher teacher { get; set; }
        [ForeignKey("teacher")]
        public int teacherid { get; set; }
        public virtual coursescategories coursescategories { get; set; }
        [ForeignKey("coursescategories")]
        public int catigoriesid { get; set; }

    }
}