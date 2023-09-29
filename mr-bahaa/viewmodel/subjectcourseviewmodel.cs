using mr_bahaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mr_bahaa.viewmodel
{
    public class subjectcourseviewmodel
    {
        public subjectcoure subjectcoure { get; set; }
        public subjectcoureimg img { get; set; }
        public List<lecture> lectures { get; set; }
        public List<exam> exams { get; set; }
    }
}