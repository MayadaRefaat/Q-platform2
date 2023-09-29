using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class lectureviewmodel
    {
        public lecture lecture { get; set; }
        public List<photolec> img { get; set; }
        public List<photolec> vid { get; set; }
        public List<photolec> aud { get; set; }
        public List<photolec> pdf { get; set; }
        public List<string> iframe { get; set; }

    }
}