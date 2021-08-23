using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Sber
{
    public class SberResult
    {
        public string orderId { get; set;}
        public string formUrl { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
