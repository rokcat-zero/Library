using Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Library.IServices
{
    public class EmailServices
    {
        public void GeneratedMail(string UsrEmail, bool Bbc, string Theme, string NameTPL, Dictionary<string, string> BodyCont, List<Attachment> attachments)
        {
            MailOptions mailOptions = new MailOptions(); 
            Startup.StatConfig.GetSection("Mail").Bind(mailOptions);
            MailAddress mailAddress = new MailAddress(mailOptions.login);
            MailMessage mailMessage = new MailMessage();

            if (string.IsNullOrWhiteSpace(UsrEmail))
            {
                mailMessage.To.Add(mailOptions.bbc);
            }
            else
            {
                mailMessage.To.Add(UsrEmail);
            }
            
            mailMessage.From = mailAddress;
            mailMessage.Subject = string.Format("CoralBonus: {0}", Theme);
            mailMessage.IsBodyHtml = mailOptions.html;
            StringBuilder stringBuilder = new StringBuilder();

            using (var reader = System.IO.File.OpenText(Environment.CurrentDirectory + string.Format(@"\wwwroot\emailTemplates\{0}.html", NameTPL)))
            {
                stringBuilder.Append(reader.ReadToEnd());
            }
            foreach (var itm in BodyCont)
            {
                stringBuilder.Replace(itm.Key, itm.Value);
            }
            if (attachments != null)
            {
                foreach(var attach in attachments.ToList())
                {
                    mailMessage.Attachments.Add(attach);
                }
                
            }
            mailMessage.Body = stringBuilder.ToString();

            

            Send(mailMessage, mailOptions);
            
        }
        
        private bool Send(MailMessage mailMessage, MailOptions mailOptions)
        {
            using (SmtpClient smtp = new SmtpClient(mailOptions.smtp, mailOptions.smtpport))
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(mailOptions.login, mailOptions.pwd);
                smtp.EnableSsl = mailOptions.ssl;
                smtp.Send(mailMessage);
                try
                {
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}
