using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        List<Produkt> bonList = new List<Produkt>();
        Produktgruppe CurrentShownGroup;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            Debug.WriteLine("hej");
        }
        private void BTN_HentSetup_Click(object sender, RoutedEventArgs e)
        {
            setup.GetSetup("1");
            IC_Produktgrupper.ItemsSource = DataStore.Produktgrupper.Values.ToList();

        }
        private void BTN_Produktgruppe_Click(object sender, RoutedEventArgs e)
        {
            Button x = (Button)sender;
            string tag = x.Tag.ToString();
            Debug.WriteLine(x.Tag);
            int id = Convert.ToInt32(tag);
            if(DataStore.Produktgrupper.ContainsKey(id))
            {
                IC_Produkter.ItemsSource = DataStore.Produktgrupper[Convert.ToInt32(tag)].Produkter.Values.ToList();
                CurrentShownGroup = DataStore.Produktgrupper[Convert.ToInt32(tag)];
            }
        }
        private void BTN_Produkt_Click(object sender, RoutedEventArgs e)
        {
            Button x = (Button)sender;
            string tag = x.Tag.ToString();
            Debug.WriteLine(x.Tag);
            int id = Convert.ToInt32(tag);
            if (CurrentShownGroup.Produkter.ContainsKey(id))
            {
                bonList.Add(CurrentShownGroup.Produkter[id]);
            }

            LV_Bonvindue.Items.Add(CurrentShownGroup.Produkter[id]);
        }

    }
}
