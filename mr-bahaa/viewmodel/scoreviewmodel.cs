using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class scoreviewmodel
    {
        public Student  student { get; set; }
        public List<score> scores { get; set; }
        public double total { get; set; }
        public string img { get; set; }
        public List<quesscore> quesscore { get; set; }
        public List<absenseviewmodel> absenseviewmodels { get; set; }
        public List<homework> homeworks { get; set; }


    }
}