using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class studentquestionviewmodel
    {
        public studentquestions studentquestions { get; set; }
        public studnetquestionphoto studnetquestionphoto { get; set; }
        public Student   student { get; set; }
        public course   course { get; set; }
    }
}