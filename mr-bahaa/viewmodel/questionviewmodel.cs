using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class questionviewmodel
    {
        public exam exam { get; set; }
        public List<qeustioncheakpoint> questions { get; set; }
        public List<Essayquestion> essayquestions { get; set; }
        public double time  { get; set; }
        public string  mass { get; set; }
        public questions question { get; set; }
        public img img { get; set; }

    }
}