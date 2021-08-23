using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Sber
{
    public class SberResultPayment
    {
        public string expiration { get; set; }
        public string cardholderName { get; set; }
        public string depositAmount { get; set; }
        public string currency { get; set; }
        public string approvalCode { get; set; }
        public int authCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int OrderStatus { get; set; }
        public string OrderNumber { get; set; }
        public string Pan { get; set; }
        public int Amount { get; set; }
        public string Ip { get; set; }

    }
}
