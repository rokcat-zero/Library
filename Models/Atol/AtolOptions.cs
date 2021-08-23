using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Atol
{
    public class AtolOptions
    {
        public string mode { get; set; }
        public AtolOptionsSettings life { get; set; }
        public AtolOptionsSettings test { get; set; }
    }
    public class AtolOptionsSettings
    {
        public string login { get; set; }
        public string pass { get; set; }
        public string lnk { get; set; }
        public string code_group { get; set; }
        public string email { get; set; }
        public string sno { get; set; }
        public string inn { get; set; }
        public string payment_address { get; set; }

    }
}
