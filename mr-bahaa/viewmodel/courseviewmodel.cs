using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class courseviewmodel
    {
        public course course { get; set; }
        public List<Student> students { get; set; }
    }
}