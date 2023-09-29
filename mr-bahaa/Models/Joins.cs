using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class Joins
    {
        public int Joinsid { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int Studentid { get; set; }
        public virtual subjectcoure subjectcoure { get; set; }
        [ForeignKey("subjectcoure")]
        public int subjectcoureid { get; set; }
        public bool apporved { get; set; }
    }
}