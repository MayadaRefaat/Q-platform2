using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class studentquestionreply
    {
        public studentquestions studentquestions { get; set; }
        public studnetquestionphoto studnetquestionphoto { get; set; }
        public replayphotos replayphotos { get; set; }
    }
}