using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Atol
{
    public class AtolOptionsItems
    {
        public string name { get; set;}
        public decimal price { get; set; }
        public decimal quantity { get; set; }
        public decimal sum { get; set; }
        public string measurement_unit { get; set; }
        public string payment_method { get; set; }
        public string payment_object { get; set; }
        public string vat { get; set; }

    }
}
