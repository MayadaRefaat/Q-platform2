using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class lecturelock
    {
        [Key]
        public int lecturelockid { get; set; }
        public virtual lecture lecture { get; set; }
        [ForeignKey("lecture")]
        public int lectureid { get; set; }
        public string studentid { get; set; }
        [DataType(DataType.Date)]
        public DateTime entered { get; set; }

    }
}