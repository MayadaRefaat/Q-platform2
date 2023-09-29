using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class lecture
    {
        [Key]
        public int lectureid { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string text { get; set; }
        public List<string> fileurl { get; set; }
        public int examid { get; set; }
        public int secexamid { get; set; }
        public bool  homework { get; set; }
        public int subjectid { get; set; }




    }
}