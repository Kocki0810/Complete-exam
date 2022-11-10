using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exam_terminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        HentSetup setup = new HentSetup();
        ObservableCollection<Produkt> bonList = new ObservableCollection<Produkt>();
        public static Ekspedient ekspeident = new Ekspedient();

        Produktgruppe CurrentShownGroup;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            Debug.WriteLine("hej");
            LV_Bonvindue.ItemsSource = bonList;
        }
        public void GetDataFromAPI()
        {
            setup.GetProdukter();
            setup.GetEkspedienter();
            IC_Produktgrupper.ItemsSource = DataStore.OC_Produktgrupper;
        }
        private void BTN_Produktgruppe_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();
            Debug.WriteLine(btn.Tag);
            int id = Convert.ToInt32(tag);
            if(DataStore.OC_Produktgrupper.Where(x => x.id == id).Count() != 0)
            {
                IC_Produkter.ItemsSource = DataStore.OC_Produktgrupper.Where(x => x.id == id).First().Produkter.Values;
                CurrentShownGroup = DataStore.OC_Produktgrupper.Where(x => x.id == id).First();
            }
        }
        private void BTN_Produkt_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();
            Debug.WriteLine(btn.Tag);
            int id = Convert.ToInt32(tag);
            Produkt produkt = (Produkt)CurrentShownGroup.Produkter[id].Clone();
            int AmountToAdd;
            if(!Int32.TryParse(TB_KeyboardInput.Text, out AmountToAdd))
            {
                AmountToAdd = 1;
            }
            if (CurrentShownGroup.Produkter.ContainsKey(id))
            {
                bool AlreadyExists = false;
                foreach (Produkt p in bonList)
                {
                    if(p.id == produkt.id)
                    {
                        p.antal += AmountToAdd;
                        p.pris = produkt.pris * p.antal;
                        AlreadyExists = true;
                        break;
                    }
                }
                if(!AlreadyExists)
                {
                    produkt.antal += AmountToAdd;
                    produkt.pris = produkt.antal * produkt.pris;
                    bonList.Add(produkt);
                }
            }
            LV_Bonvindue.ScrollIntoView(produkt);
            LB_OrdreSum_Tal.Content = bonList.Sum(p => p.pris).ToString();
            TB_KeyboardInput.Text = "";
        }
        private void BTN_Numpad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Tag.ToString() == "C")
            {
                TB_KeyboardInput.Text = "";
                return;
            }
            else if(btn.Tag.ToString() == "ENT")
            {
                if(DataStore.Firmaid == "-1")
                {
                    DataStore.Firmaid = TB_KeyboardInput.Text;
                    TB_KeyboardInput.Text = "";
                    GetDataFromAPI();
                    LB_indtast_firmaid.Visibility = Visibility.Hidden;
                }
                return;
            }
            TB_KeyboardInput.Text += btn.Tag.ToString();

        }

        private void BTN_LoadEkspedient_Click(object sender, RoutedEventArgs e)
        {
            Window_LoadEkspedient ekspedient_window = new Window_LoadEkspedient(this);
            ekspedient_window.Show();
        }

        private void BTN_Slet_Produkt_BonList_Click(object sender, RoutedEventArgs e)
        {
            if(LV_Bonvindue.SelectedIndex != -1)
            {
                bonList.RemoveAt(LV_Bonvindue.SelectedIndex);
            }
            LB_OrdreSum_Tal.Content = bonList.Sum(p => p.pris).ToString();
        }

        private void BTN_Betal_Click(object sender, RoutedEventArgs e)
        {

            Produkt[] produkts = new Produkt[bonList.Count];
            bonList.CopyTo(produkts, 0);
            setup.SendBetaling(produkts);
            bonList.Clear();
            LB_OrdreSum_Tal.Content = "";
        }

        private void BTN_ReloadData_Click(object sender, RoutedEventArgs e)
        {
            DataStore.OC_Produktgrupper.Clear();
            IC_Produkter.ItemsSource = null;
            bonList.Clear();
            LB_OrdreSum_Tal.Content = "";
            GetDataFromAPI();
        }
    }
}
