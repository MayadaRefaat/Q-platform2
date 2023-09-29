using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class lecturecontrolview
    {
        [Key]
        public int controlid { get; set; }
        public virtual lecture lecture { get; set; }
        [ForeignKey("lecture")]
        public int lectureid { get; set; }
        public virtual course course { get; set; }
        [ForeignKey("course")]
        public int courseid { get; set; }
        public bool view { get; set; }
    }
}