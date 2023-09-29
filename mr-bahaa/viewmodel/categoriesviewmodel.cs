using mr_bahaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mr_bahaa.viewmodel
{
    public class categoriesviewmodel
    {
        public coursescategories coursescategories { get; set; }
        public List<subjectcourseviewmodel> subjectcourseviewmodel { get; set; }
    }
}