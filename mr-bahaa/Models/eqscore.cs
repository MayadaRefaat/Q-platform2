using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class eqscore
    {

        [Key]
        public int eqscoreid { get; set; }
        public int eqeustionid { get; set; }
        public string  answar { get; set; }
        public string userid { get; set; }
        public int degree { get; set; }
        public int examid { get; set; }
    }
}