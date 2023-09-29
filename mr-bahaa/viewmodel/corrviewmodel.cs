using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class corrviewmodel
    {
        public essayexam  essayexam { get; set; }
        public List<Student> students { get; set; }
    }
}