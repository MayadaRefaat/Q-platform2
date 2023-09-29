using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class quese_score
    {
        [Key]
        public int qscoreid { get; set; }
        public int qpoints { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int id { get; set; }
        public virtual exam   exam { get; set; }
        [ForeignKey("exam")]
        public int examid { get; set; }
        public virtual questions questions { get; set; }
        [ForeignKey("questions")]
        public int questionid { get; set; }

    }
}