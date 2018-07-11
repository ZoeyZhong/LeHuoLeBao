using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeHuoLeBao.Models
{
    public class ParkCom
    {
        public decimal park_id { get; set; }
        public string p_title { get; set; }
        public string p_content { get; set; }
        public bool isDel { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
    }
}