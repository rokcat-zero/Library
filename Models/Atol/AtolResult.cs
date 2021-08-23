using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Atol
{
    public class AtolResult
    {
        public string uuid { get; set; }
        public string timestamp { get; set; }
        public AtolResultError error { get; set; }
        public string status { get; set; }

    }
    public class AtolResultError
    {
        public string error_id { get; set; }
        public int code { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }

}
