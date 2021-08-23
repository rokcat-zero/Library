using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Library.Models.Sber;
using Microsoft.Extensions.Configuration;

namespace Library.Services
{
    public class SberServices
    {
        #region Create order
        public string CreateOrder(int OrderId, decimal OrderPrice, decimal OrderBonus, string OrderEmail, string ReturnUrl, string FailUrl)
        {
            var sberOptions = new SberOptions();
            Startup.StatConfig.GetSection("Sber").Bind(sberOptions);
            Dictionary<string, string> urlContext = new Dictionary<string, string>();
            string paymentUrl = string.Empty;
            if (sberOptions.mode == "life")
            {
                urlContext.Add("userName", sberOptions.life.lgn);
                urlContext.Add("password", sberOptions.life.pwd);
                paymentUrl = sberOptions.life.lnk;
            }
            else
            {
                urlContext.Add("userName", sberOptions.test.lgn);
                urlContext.Add("password", sberOptions.test.pwd);
                paymentUrl = sberOptions.test.lnk;
            }
            urlContext.Add("orderNumber", string.Format("eo{0}", OrderId));

            decimal orPrice = decimal.Zero;
            if (OrderBonus != 0)
            {
                orPrice = OrderPrice - OrderBonus;
            }
            else
            {
                orPrice = OrderPrice;
            }

            urlContext.Add("amount", string.Format("{0}00", orPrice.ToString("0")));
            urlContext.Add("returnUrl", ReturnUrl);
            urlContext.Add("failUrl", FailUrl);
            var urlBuild = string.Format("payment/rest/register.do?{0}",
                    string.Join("&", urlContext.Select(c => string.Format("{0}={1}", c.Key, c.Value)))
                );
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(paymentUrl);

                using (HttpResponseMessage responseMessage = client.GetAsync(urlBuild).Result)
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        return result.ToString();
                    }
                }
            }
            return string.Empty;
        }
        #endregion
        #region Check order
        public string CheckOrder(string orderUID)
        {
            var sberOptions = new SberOptions();
            Startup.StatConfig.GetSection("Sber").Bind(sberOptions);
            Dictionary<string, string> urlContext = new Dictionary<string, string>();
            string paymentUrl = string.Empty;
            if (sberOptions.mode == "life")
            {
                urlContext.Add("userName", sberOptions.life.lgn);
                urlContext.Add("password", sberOptions.life.pwd);
                paymentUrl = sberOptions.life.lnk;
            }
            else
            {
                urlContext.Add("userName", sberOptions.test.lgn);
                urlContext.Add("password", sberOptions.test.pwd);
                paymentUrl = sberOptions.test.lnk;
            }
            urlContext.Add("orderId", orderUID);
            var urlBuild = string.Format("payment/rest/getOrderStatus.do?{0}",
                   string.Join("&", urlContext.Select(c => string.Format("{0}={1}", c.Key, c.Value)))
               );
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(paymentUrl);
                using (HttpResponseMessage responseMessage = client.GetAsync(urlBuild).Result)
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        return result.ToString();
                    }
                }
            }
            return string.Empty;
        }
        #endregion
    }
}
