using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Sber
{
    public class SberOptions
    {
        public string mode { get; set; }
        public SberSetting life { get; set; }
        public SberSetting test { get; set; }
        public string OrderCreate { get; set; }
        public string OrderCheck { get; set; }
    }
    public class SberSetting
    {
        public string lgn { get; set; }
        public string pwd { get; set; }
        public string lnk { get; set; }
        public string returnUrl { get; set; }
        public string failUrl { get; set; }
    }
}
