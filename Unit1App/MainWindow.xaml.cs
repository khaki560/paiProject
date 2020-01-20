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
    /// 

    class UnitEntryDisplay : UnitEntry
    {

        public bool Selected { set; get; }

        public UnitEntryDisplay(UnitEntry a) : base(a.Id, a.Name, a.Count, a.Price)
        {
            Selected = false;
        }

        public UnitEntry GetUnitEntry()
        {
            return new UnitEntry(this.Id, this.Name, this.Count, this.Price);
        }
    }
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
                var addPopUp = new Add();
                addPopUp.ShowDialog();

                client.Unit.AddMagazineProduct(addPopUp.Name, addPopUp.Count, addPopUp.Price);
                RefreshListOfEntires();
            }
            catch
            {
                var errorPopUp = new ErrorPopUp();
                errorPopUp.ShowDialog();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                foreach (UnitEntryDisplay item in ListOfEntires.Items)
                {
                    if (item.Selected == true)
                    {
                        client.Unit.RemoveMagazineProduct(item.Id.Value);
                    }
                }
                RefreshListOfEntires();
            } 
            catch
            {
                var errorPopUp = new ErrorPopUp();
                errorPopUp.ShowDialog();
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                foreach (UnitEntryDisplay item in ListOfEntires.Items)
                {
                    if (item.Selected == true)
                    {
                        var modifyPopUp = new ModifyPopUp();
                        modifyPopUp.ShowDialog();

                        client.Unit.ModifyMagazineProduct(item.Id.Value, modifyPopUp.Count, modifyPopUp.Price);
                    }
                }
                RefreshListOfEntires();
            }
            catch
            {
                var errorPopUp = new ErrorPopUp();
                errorPopUp.ShowDialog();
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
                    ListOfEntires.Items.Add(new UnitEntryDisplay(item));
                }
            }
            catch
            {
                var errorPopUp = new ErrorPopUp();
                errorPopUp.ShowDialog();
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
                var errorPopUp = new ErrorPopUp();
                errorPopUp.ShowDialog();
            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();
                foreach (UnitEntryDisplay item in ListOfEntires.Items)
                {
                    if (item.Selected == true)
                    {
                        var orderPopUp = new OrderPopUp();
                        orderPopUp.ShowDialog();

                        var count = orderPopUp.Count;


                        client.Order.Order(item.GetUnitEntry(), Int32.Parse(count));
                    }
                }
                RefreshListOfEntires();
            }
            catch
            {
                var errorPopUp = new ErrorPopUp();
                errorPopUp.ShowDialog();
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            // Not ready Yet
            return; 
        }
    }
}
