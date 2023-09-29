using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mr_bahaa.Models
{
    public class replayphotos
    {
        [Key]
        public int imgid { get; set; }
        public string imgurl { get; set; }
        [Required]
        public int questionid { get; set; }
        public string type { get; set; }
    }
}