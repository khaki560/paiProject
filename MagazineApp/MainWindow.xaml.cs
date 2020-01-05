using Microsoft.Rest;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagazineApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public const string SERVICE_URL = "https://localhost:44315/";

        public MainWindow()
        {
            InitializeComponent();
            RefreshListOfEntires();
        }

        public static MagazineWebService2 GetWebClient(string uri = SERVICE_URL)
        {
            var client = new MagazineWebService2(new Uri(uri), new BasicAuthenticationCredentials());
            return client;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var client = GetWebClient();

            string name = NameValue.Text;
            int count = Int32.Parse(CountValue.Text);
            string localization = LocalizationValue.Text;

            client.Magazine.AddMagazineProduct(name, count, localization);
            RefreshListOfEntires();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var client = GetWebClient();

            int id = Int32.Parse(idValue.Text);
            int count = Int32.Parse(CountValue.Text);
            string localization = LocalizationValue.Text;

            client.Magazine.RemoveMagazineProduct(id);
            RefreshListOfEntires();
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            var client = GetWebClient();

            int id = Int32.Parse(idValue.Text);
            int count = Int32.Parse(CountValue.Text);

            client.Magazine.ModifyMagazineProduct(id, count);
            RefreshListOfEntires();
        }

        private void RefreshListOfEntires()
        {
            var client = GetWebClient();
            var list = client.Magazine.GetAllProducts();

            ListOfEntires.Items.Clear();

            foreach (var item in list)
            {
                ListOfEntires.Items.Add(item);
            }
        }
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshListOfEntires();
        }
    }
}
