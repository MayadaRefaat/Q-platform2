using mr_bahaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mr_bahaa.viewmodel
{
    public class ggogleformviewmodel
    {
        public List<questions> question { get; set; }
        public int degree { get; set; }
        public int total { get; set; }
        public string text { get; set; }
    }
}