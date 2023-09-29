using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class qcontrol
    {
        [Key]
        public int qcontrolid { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int id { get; set; }
        public virtual questions Questions { get; set; }
        [ForeignKey("Questions")]
        public int questionid { get; set; }
    }
}