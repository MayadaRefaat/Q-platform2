using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class score
    {
        [Key]
        public int scoreid { get; set; }
        [Required]
        public double points { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int id { get; set; }



    }
}