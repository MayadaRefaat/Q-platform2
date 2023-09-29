using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mr_bahaa.Models;

namespace mr_bahaa.viewmodel
{
    public class roleviewmodel
    {
        public List<ApplicationUser> users { get; set; }
        public string roleid  { get; set; }
    }
}