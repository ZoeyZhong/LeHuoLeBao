using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeHuoLeBao.Models
{
    public class NewsCom
    {
        public decimal newsid { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime? time { get; set; }
        public bool isDel { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
    }
}