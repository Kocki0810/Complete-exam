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

namespace exam_terminal
{
    public class HentSetup
    {
        public string GetUrl(string table, string[] includes, string firmaid)
        {
            string include = includes.Aggregate("", (a, b) => a + "&include" + b.ToLower() + "=1");
            return $"http://localhost:8000/api/v1/{table}?firma_id[eq]={firmaid}{include}";
        }
        async public void GetSetup(string firmaid)
        {
            GetUrl("bruger", new string[] { "ekspedient", "test" }, "1");

            using HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.GetAsync(new Uri(GetUrl("produktgruppe", new string[] { "produkt" }, firmaid)));
            string content = await response.Content.ReadAsStringAsync();
            JsonTextReader reader = new JsonTextReader(new StringReader(content));
            reader.Read();
            JObject o2 = (JObject)JToken.ReadFrom(reader);
            
            Produktgruppe p = JsonConvert.DeserializeObject<Produktgruppe>(content);
            List<Produktgruppe> produktgruppeList = o2["data"].Children().Select(i => i.ToObject<Produktgruppe>()).ToList();
            List<Produkt> ProduktList = o2["data"].Children()["produkt"].Children().Select(i => i.ToObject<Produkt>()).ToList();
            foreach(Produktgruppe gruppe in produktgruppeList)
            {
                foreach (Produkt produkt in ProduktList)
                {
                    if(produkt.produktgruppe_id == gruppe.id)
                    {
                        gruppe.Produkter.Add(produkt.id, produkt);
                    }
                }
            }
            DataStore.Produktgrupper = produktgruppeList.ToDictionary(gruppe => gruppe.id);

            //dataStore.Produktgrupper = JsonConvert.DeserializeObject<Produktgruppe>(o2["data"].ToString());

        }

    }
}