using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeHuoLeBao.Models
{
    public class RecuitCom
    {
        public decimal recruitid { get; set; }
        public string re_name { get; set; }
        public string re_type { get; set; }
        public string re_money { get; set; }
        public string re_require { get; set; }
        public bool isDel { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
    }
}