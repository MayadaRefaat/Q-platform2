using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class studentviewmodel
    {
        public Student student { get; set; }
        public List<course> courses { get; set; }
    }
}