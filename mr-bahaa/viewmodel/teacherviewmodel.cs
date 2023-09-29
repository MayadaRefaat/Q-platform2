using mr_bahaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mr_bahaa.viewmodel
{
    public class teacherviewmodel
    {
        public Teacher teacher { get; set; }
        public teacherimg img { get; set; }
        public List<subjectcourseviewmodel> courses { get; set; }
    }
}