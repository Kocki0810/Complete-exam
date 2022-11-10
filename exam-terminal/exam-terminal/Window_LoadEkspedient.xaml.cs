using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace exam_terminal
{
    /// <summary>
    /// Interaction logic for Window_LoadEkspedient.xaml
    /// </summary>
    public partial class Window_LoadEkspedient : Window
    {
        public string kortnummer;
        MainWindow window;
        public string result;
        public Window_LoadEkspedient(MainWindow window)
        {
            this.window = window;
            DataContext = this;
            InitializeComponent();
            IC_Ekspedienter.DataContext = DataStore.OC_Ekspedienter;
            IC_Ekspedienter.ItemsSource = DataStore.OC_Ekspedienter;

            TB_Kortnummer.Focus();
        }
        private void BTN_Ekspedient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_OK_Click(object sender, RoutedEventArgs e)
        {
            kortnummer = TB_Kortnummer.Text.Substring(1, TB_Kortnummer.Text.Length-2);
            MainWindow.ekspeident = DataStore.LoadEkspedientByKortnr(kortnummer);
            if(MainWindow.ekspeident == null)
            {
                //bla bla
                LB_Result.Content = "Kan ikke finde ekspedient med det nummer";
                return;
            }
            window.LB_Ekspedient_Navn.Content = "Ekspedient: " + MainWindow.ekspeident.navn;
            Close();
        }
    }
}
