﻿using mr_bahaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mr_bahaa.viewmodel
{
    public class studentipviewmodel
    {
        public Student Student { get; set; }
        public List<studentip> ip { get; set; }
    }
}