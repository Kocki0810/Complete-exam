using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_terminal
{
    public static class DataStore
    {
        //public static Dictionary<int, Produktgruppe> Produktgrupper = new Dictionary<int, Produktgruppe>();
        //public static Dictionary<int, Ekspedient> Ekspedienter = new Dictionary<int, Ekspedient>();
        public static ObservableCollection<Produktgruppe> OC_Produktgrupper = new ObservableCollection<Produktgruppe>();
        public static ObservableCollection<Ekspedient> OC_Ekspedienter = new ObservableCollection<Ekspedient>();
        public static string Firmaid = "-1";
        public static Ekspedient LoadEkspedientByKortnr(string kortnr)
        {
            if (OC_Ekspedienter.Where(x => x.kortnummer == kortnr).ToList().Count == 1)
            {
                return OC_Ekspedienter.Where(x => x.kortnummer == kortnr).First();
            }
            return null;
        }

    }
}
