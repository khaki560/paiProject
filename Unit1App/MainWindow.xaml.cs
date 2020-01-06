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
using Unit1App.Models;

namespace Unit1App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string SERVICE_URL = "https://localhost:44346/";

        public MainWindow()
        {
            InitializeComponent();
            RefreshListOfEntires();
        }

        public static UnitWebService GetWebClient(string uri = SERVICE_URL)
        {
            return new UnitWebService(new Uri(uri), new BasicAuthenticationCredentials());
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                string name = NameValue.Text;
                int count = Int32.Parse(CountValue.Text);
                float price = float.Parse(PriceValue.Text);

                client.Unit.AddMagazineProduct(name, count, price);
                RefreshListOfEntires();
            }
            catch
            {

            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                int id = Int32.Parse(idValue.Text);

                client.Unit.RemoveMagazineProduct(id);
                RefreshListOfEntires();
            }
            catch
            {

            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                int id = Int32.Parse(idValue.Text);
                int count = Int32.Parse(CountValue.Text);
                float price = float.Parse(PriceValue.Text);

                client.Unit.ModifyMagazineProduct(id, count, price);
                RefreshListOfEntires();
            }
            catch
            {

            }
        }

        private void RefreshListOfEntires()
        {
            try
            {
                var client = GetWebClient();
                var list = client.Unit.GetAllProducts();

                ListOfEntires.Items.Clear();

                foreach (var item in list)
                {
                    ListOfEntires.Items.Add(item);
                }
            }
            catch
            {

            }
        }
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshListOfEntires();
            }
            catch
            {

            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                int id = Int32.Parse(idValue.Text);
                int count = Int32.Parse(CountValue.Text);
                float price = float.Parse(PriceValue.Text);
                string name = NameValue.Text;

                var entry = new UnitEntry(id, name, 0, price);

                client.Order.Order(entry, count);
                RefreshListOfEntires();
            }
            catch
            {

            }
        }
    }
}
