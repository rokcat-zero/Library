using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Library.Models.Atol;
using Microsoft.Extensions.Configuration;

namespace Library.Services
{
    public class AtolServices
    {
        #region GetToken
        private AtolResultToken AtolToken()
        {
            AtolOptions atolOptions = new AtolOptions();
            Startup.StatConfig.GetSection("Atol").Bind(atolOptions);
            AtolOptionsSettings atolOptionsSettings = new AtolOptionsSettings();
            if (atolOptions.mode == "life")
            {
                atolOptionsSettings = atolOptions.life;
            }
            else
            {
                atolOptionsSettings = atolOptions.test;
            }
            atolOptionsSettings.lnk = atolOptionsSettings.lnk + "getToken";
            JObject obj = new JObject(
                new JProperty("login", atolOptionsSettings.login),
                new JProperty("pass", atolOptionsSettings.pass)
                );
            var result = AtolPostData(atolOptionsSettings.lnk, obj.ToString(), null);
            if (!string.IsNullOrWhiteSpace(result))
            {
                AtolResultToken atolResultToken = JsonConvert.DeserializeObject<AtolResultToken>(result);
                return atolResultToken;
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region generationOrder
        public string AtolSendBillPartner(int orderId, List<AtolOptionsPayment> PaymentObj, string ClientEmail, decimal OrderPrice, AtolOptionsItems atolOptionsItems, string AccNumber, string BonusValue)
        {
            AtolResultToken token = AtolToken();
            if (!string.IsNullOrWhiteSpace(token.token))
            {
                AtolOptions atolOptions = new AtolOptions();
                Startup.StatConfig.GetSection("Atol").Bind(atolOptions);
                AtolOptionsSettings atolOptionsSettings = new AtolOptionsSettings();
                if (atolOptions.mode == "life")
                {
                    atolOptionsSettings = atolOptions.life;
                }
                else
                {
                    atolOptionsSettings = atolOptions.test;
                }

                JArray arItems = new JArray();
                JArray arPayment = new JArray();
                foreach (var itm in PaymentObj)
                {
                    JObject pm = new JObject
                    {
                        new JProperty("type", itm.type),
                        new JProperty("sum", itm.sum)
                    };
                    arPayment.Add(pm);
                }
                JObject jsItem = new JObject(
                    new JProperty("name", atolOptionsItems.name),
                    new JProperty("price", Math.Round(atolOptionsItems.price, 2)),
                    new JProperty("quantity", Math.Round(atolOptionsItems.quantity, 1)),
                    new JProperty("sum", Math.Round(atolOptionsItems.sum, 2)),
                    new JProperty("measurement_unit", "Штука"),
                    new JProperty("payment_method", atolOptionsItems.payment_method),
                    new JProperty("payment_object", atolOptionsItems.payment_object),
                    new JProperty("vat", new JObject(new JProperty("type", "none")))
                );
                arItems.Add(jsItem);
                JObject jsObject = new JObject(
                new JProperty("external_id", string.Format("p{0}", orderId)),
                new JProperty("timestamp", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")),
                new JProperty("service", new JObject(
                    new JProperty("callback_url", atolOptionsSettings.payment_address))
                ),
                new JProperty("receipt", new JObject(
                        new JProperty("client", new JObject(
                            new JProperty("email", ClientEmail))),
                        new JProperty("company", new JObject(
                            new JProperty("email", atolOptionsSettings.email),
                            new JProperty("sno", atolOptionsSettings.sno),
                            new JProperty("inn", atolOptionsSettings.inn),
                            new JProperty("payment_address", atolOptionsSettings.payment_address))),
                        new JProperty("items", arItems),
                        new JProperty("payments", arPayment),
                        new JProperty("total", atolOptionsItems.sum),
                        new JProperty("additional_user_props", new JObject(
                            new JProperty("name", string.Format("Остаток бонусов на счете {0}", AccNumber)),
                            new JProperty("value", BonusValue)
                            ))
                ))

                );
                atolOptionsSettings.lnk = atolOptionsSettings.lnk + atolOptionsSettings.code_group + "/sell";
                var result = AtolPostData(atolOptionsSettings.lnk, jsObject.ToString(), token.token);
                return result;
            }
            return new string("Token not found");
        }
        #endregion
        #region send data 
        private string AtolPostData(string url, string jsData, string Token)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(Token))
                {
                    client.DefaultRequestHeaders.Add("Token", Token);
                }
                Uri uri = new Uri(url);
                HttpContent content = new StringContent(jsData, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = client.PostAsync(uri, content).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return new string(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        return new string(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }
        #endregion
    }
}
