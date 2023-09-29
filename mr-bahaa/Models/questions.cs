using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class questions
    {
        [Key]
        public int questionid { get; set; }
        [DataType(DataType.MultilineText)]
        public string question { get; set; }
        public string questiongrade { get; set; }
        public string first { get; set; }
        public string sec { get; set; }
        public string third { get; set; }
        public string fourth { get; set; }
        public string correct { get; set; }
        public string choise { get; set; }
        [DataType(DataType.MultilineText)]
        public string Explanation { get; set; }
        public int examid { get; set; }


    }
}