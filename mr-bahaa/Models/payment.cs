using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class payment
    {
        [Key]
        public int paymentid { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int id { get; set; }

    }
}