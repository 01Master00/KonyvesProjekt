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
        private static Adatbazis adatbazis = new Adatbazis();

        internal static Adatbazis Adatbazis { get => adatbazis; set => adatbazis = value; }

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                adatbazis = new Adatbazis();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az adatbázis kapcsolat létrehozásakor: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dg_adatok.ItemsSource = adatbazis.Konyvek;
            btn_beolvas.IsEnabled = false;
            btn_ujkonyv.IsEnabled = true;
            btn_Ktorol.IsEnabled = true;
        }

        private void btn_ujkonyv_Click(object sender, RoutedEventArgs e)
        {
            Window1 ujkonyv = new Window1();
            ujkonyv.ShowDialog();
            dg_adatok.ItemsSource = null;
            dg_adatok.ItemsSource = adatbazis.Konyvek;
            CollectionViewSource.GetDefaultView(dg_adatok.ItemsSource).Refresh();
        }

        private void btn_Ktorol_Click(object sender, RoutedEventArgs e)
        {
            if (dg_adatok.SelectedItem is Konyv selectedKonyv)
            {
                adatbazis.Konyvek.Remove(selectedKonyv);
                adatbazis.TorolKonyv(selectedKonyv);
                dg_adatok.ItemsSource = adatbazis.Konyvek;
                dg_adatok.Items.Refresh();
            }
        }


    }
}