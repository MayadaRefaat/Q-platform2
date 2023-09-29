using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class Teacher
    {
        [Key]
        public int teacherid { get; set; }
        [Required]
        public string name   { get; set; }
        [Required]
        public string info { get; set; }
        [Required]
        public string subject { get; set; }
    }
}