using mr_bahaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mr_bahaa.viewmodel
{
    public class studentcoursesviewmodel
    {
        public List<Joins> Joins { get; set; }
        public Student  student { get; set; }
    }
}