using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class examtime
    {
        [Key]
        public int examtimeid { get; set; }
        public virtual exam exam { get; set; }
        [ForeignKey("exam")]
        public int examid { get; set; }
        public virtual Student  Student { get; set; }
        [ForeignKey("Student")]
        public int Studentid { get; set; }
        public DateTime timeodentring  { get; set; }
    }
}