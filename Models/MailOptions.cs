using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class MailOptions
    {
        public string login { get; set; }
        public string pwd { get; set; }
        public string smtp { get; set; }
        public int smtpport { get; set; }
        public bool html { get; set; }
        public string bbc { get; set; }
        public bool ssl { get; set; }
    }
    
    public class MailTemplateName
    {
        public string Name { get; set; }
    }

}
