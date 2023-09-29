using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class studentquestions
    {
        [Key]
        public int studentquestionid { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(Name =" Write your question ")]
        [Required]
        public string studentquestion { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string teacherreplay { get; set; }
        public string studentid { get; set; }
        public int teacherid { get; set; }


    }
}