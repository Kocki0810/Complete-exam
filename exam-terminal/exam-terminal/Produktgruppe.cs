using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace exam_terminal
{
    public class Produktgruppe
    {
        public int id { get; set; }
        public string Navn { get; set; }
        public Dictionary<int, Produkt> Produkter = new Dictionary<int, Produkt>();

    }
}