using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class homework
    {
        [Key]
        public int homeworkid { get; set; }
        public string text { get; set; }
        public string Evaluation { get; set; }

        public virtual lecture lecture { get; set; }
        [ForeignKey("lecture")]
        public int lectureid { get; set; }

    
        public string studentid { get; set; }


    }
}