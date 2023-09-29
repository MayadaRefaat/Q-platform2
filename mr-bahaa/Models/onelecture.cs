using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class onelecture
    {
        [Key]
        public int oneid { get; set; }
        public virtual lecture lecture { get; set; }
        [ForeignKey("lecture")]
        public int lectureid { get; set; }
    }
}