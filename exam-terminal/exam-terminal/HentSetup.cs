using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace exam_terminal
{
    public class HentSetup
    {
        private static string token = "FinalExamBearerToken";
        public string GetUrl(string table, string[] includes)
        {
            string include = includes.Aggregate("", (a, b) => a + "&include" + b.ToLower() + "=1");
            return $"https://examwebsite.azurewebsites.net/api/v1/{table}?firma_id[eq]={DataStore.Firmaid}{include}";
        }
        public string PostUrl(string table)
        {
            return $"https://examwebsite.azurewebsites.net/api/v1/{table}";
        }
        async public void GetProdukter()
        {
            using HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.GetAsync(new Uri(GetUrl("produktgruppe", new string[] { "produkt" })));
            string content = await response.Content.ReadAsStringAsync();
            JsonTextReader reader = new JsonTextReader(new StringReader(content));
            reader.Read();
            JObject produktgrupper = (JObject)JToken.ReadFrom(reader);
            
            Produktgruppe p = JsonConvert.DeserializeObject<Produktgruppe>(content);
            List<Produktgruppe> produktgruppeList = produktgrupper["data"].Children().Select(i => i.ToObject<Produktgruppe>()).ToList();
            List<Produkt> ProduktList = produktgrupper["data"].Children()["produkt"].Children().Select(i => i.ToObject<Produkt>()).ToList();
            foreach(Produktgruppe gruppe in produktgruppeList)
            {
                foreach (Produkt produkt in ProduktList)
                {
                    if(produkt.produktgruppe_id == gruppe.id)
                    {
                        gruppe.Produkter.Add(produkt.id, produkt);
                    }
                }
                DataStore.OC_Produktgrupper.Add(gruppe);
            }
        }

        async public void GetEkspedienter()
        {
            DataStore.OC_Ekspedienter.Clear();
            using HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.GetAsync(new Uri(GetUrl("ekspedient", new string[] { })));
            string content = await response.Content.ReadAsStringAsync();
            JsonTextReader reader = new JsonTextReader(new StringReader(content));
            reader.Read();
            JObject ekspedienter = (JObject)JToken.ReadFrom(reader);
            List<Ekspedient> ekspedientList = ekspedienter["data"].Children().Select(i => i.ToObject<Ekspedient>()).ToList();
            foreach (Ekspedient ekspedient in ekspedientList)
            {
                DataStore.OC_Ekspedienter.Add(ekspedient);
            }
        }

        async public void SendBetaling(Produkt[] bonList)
        {

            object ordre = new
            {
                firma_id = Convert.ToInt32(DataStore.Firmaid),
                ekspedient_id = MainWindow.ekspeident.id,
                pris = bonList.Sum(x => x.pris),
                date = DateTime.Now.ToString("yyyy-MM-d HH:m:s")
            };
            List<object> wrapper = new List<object>();
            wrapper.Add(ordre);


            StringContent ordreContent = new StringContent(JsonConvert.SerializeObject(wrapper), Encoding.UTF8, "application/json");
            HttpRequestMessage OrdreRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(PostUrl("ordre")));
            OrdreRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            OrdreRequestMessage.Content = ordreContent;
            HttpClient client = new HttpClient();
            HttpResponseMessage OrdreResponse = await client.SendAsync(OrdreRequestMessage);
            string ordreId = await OrdreResponse.Content.ReadAsStringAsync();

            List<JObject> temp = new List<JObject>();
            foreach (Produkt p in bonList)
            {
                JObject o = JObject.FromObject(p);
                o.Add("ordre_id", ordreId.TrimStart('[').TrimEnd(']'));
                o.Add("bontekst", p.Navn);
                o.Add("produkt_id", p.id);
                o.Add("date", DateTime.Now.ToString("yyyy-MM-d HH:m:s"));
                temp.Add(o);
            }

            StringContent linjeContent = new StringContent(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json");
            HttpRequestMessage LinjeRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(PostUrl("linje")));
            LinjeRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            LinjeRequestMessage.Content = linjeContent;

            var ordreIDer = await OrdreResponse.Content.ReadAsStringAsync();
            HttpResponseMessage linjeresponse = await client.SendAsync(LinjeRequestMessage);
            //client.SendAsync();
        }
    }
}