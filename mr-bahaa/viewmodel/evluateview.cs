using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class evluateview
    {

        public Student student { get; set; }
        public homework homework { get; set; }
        public List<imghomework> imgs { get; set; }

    }
}