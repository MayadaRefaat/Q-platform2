using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class Absence
    {
        [Key]
        public int id { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int Studentid { get; set; }
        public virtual lecture lecture { get; set; }
        [ForeignKey("lecture")]
        public int lectureid { get; set; }
        public bool Presence { get; set; }
    }
}