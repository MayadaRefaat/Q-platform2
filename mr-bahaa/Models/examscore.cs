using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class examscore
    {
        [Key]
        public int id { get; set; }
        public int  examid { get; set; }
        public string studentid { get; set; }
        public int degree { get; set; }
        public int total { get; set; }

    }
}