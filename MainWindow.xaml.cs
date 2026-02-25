using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KonyvesProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Adatbazis adatbazis = new Adatbazis();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dg_adatok.ItemsSource = adatbazis.Konyvek;
            btn_beolvas.IsEnabled = false;
        }

        private void btn_ujkonyv_Click(object sender, RoutedEventArgs e)
        {
            Window1 ujkonyv = new Window1();
            ujkonyv.ShowDialog();
            dg_adatok.ItemsSource = adatbazis.Konyvek;
            dg_adatok.Items.Refresh();
        }
    }
}