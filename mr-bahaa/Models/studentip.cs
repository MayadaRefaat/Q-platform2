using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class studentip
    {
        [Key]
        public int studentipid { get; set; }
        public virtual lecture lecture { get; set; }
        [ForeignKey("lecture")]
        public int lectureid { get; set; }
        public string studentid { get; set; }
        public string ipaddress { get; set; }
    }
}