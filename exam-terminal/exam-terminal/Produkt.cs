using System.Text.Json;
using System.Text.Json.Serialization;

namespace exam_terminal
{
    public class Produkt
    {
        public string Navn { get; set; }
        public string Antal { get; set; }
        public string Pris { get; set; }
        public int produktgruppe_id { get; set; }
        public int id { get; set; }

        public string Btn_Text
        {
            get { return Navn + "\r\n" + Pris; }
        }
    }
}