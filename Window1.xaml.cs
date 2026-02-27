using System;
using System.Collections.Generic;
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

namespace KonyvesProjekt
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Konyv k;
        Adatbazis adatbazis = new Adatbazis();
        public Window1()
        {
            InitializeComponent();
            GetLastID();
        }

        private void btn_elkuld_Click(object sender, RoutedEventArgs e)
        {
            k = new Konyv(Convert.ToInt32(tb_id.Text),tb_szerzo.Text,tb_cim.Text );
            adatbazis.AddToList(k);
            adatbazis.UjKonyv(k);
            MessageBox.Show("Uj könyv felvétele megtörpént", "siekres felvétel", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {
            tb_cim.Clear();
            tb_id.Clear();
            tb_szerzo.Clear();
        }

        private int GetLastID()
        {
            int lastID = adatbazis.Konyvek.Count > 0 ? adatbazis.Konyvek.Last().Id : 0;
            tb_id.Text = (lastID + 1).ToString();
            return lastID + 1;
        }
    }
}
