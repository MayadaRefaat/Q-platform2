using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class Essayquestion
    {
        [Key]
        public int equestionid { get; set; }
        public string question { get; set; }
        public int examid { get; set; }

    }
}