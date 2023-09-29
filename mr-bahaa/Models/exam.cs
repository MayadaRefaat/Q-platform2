using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class exam
    {
        [Key]
        public int examid { get; set; }
        [Required]
        public string name  { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool open { get; set; }
        [Display(Name ="Time fo Exam")]
        public double timeofexam { get; set; }
        public int subjectid { get; set; }
        public int percentage { get; set; }
    }

}