using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace mr_bahaa.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        [Display(Name ="Name")]
        [Required]
        public string studentname { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Parent's phone number")]
        public string PhoneNumber { get; set; }
        public string secPhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        [Display(Name = "Your School")]
        public string school { get; set; }
      
        public string userid { get; set; }
        public string yourclass { get; set; }



    }
}