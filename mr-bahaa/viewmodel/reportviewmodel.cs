using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class reportviewmodel
    {
        public List<quesscore> score { get; set; }
        public exam exam { get; set; }
        public course course { get; set; }
    }
}