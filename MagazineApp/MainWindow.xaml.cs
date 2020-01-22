using MagazineApp.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MagazineApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 


    public class MagazineEntryDisplay : MagazineEntry
    {
        public bool Selected { set; get; }

        public MagazineEntryDisplay(MagazineEntry a) : base(a.Id, a.Name, a.Count, a.Localization)
        {
            Selected = false;
        }

        public MagazineEntry GetUnitEntry()
        {
            return new MagazineEntry(this.Id, this.Name, this.Count, this.Localization);
        }
    }

    public partial class MainWindow : Window
    {
        private static System.Timers.Timer aTimer;

        public const string SERVICE_URL = "https://localhost:44315/";

        public MainWindow()
        {
            InitializeComponent();
            RefreshListOfEntires();
            isSynchronize();
            SetTimer();
        }

        private void SetValue(string txt)
        {
            Synchro.Content = txt;
            if(txt == "synchronized")
            {
                RefreshListOfEntires();
            }
        }

        private void isSynchronize()
        {
            string content;
            try
            {
                var client = GetWebClient();
                var a = client.SynchronizationForUnit.IsSynchronize();


                if (a is true)
                {
                    content = "synchronized";
                }
                else
                {
                    content = "Not synchronized";
                }
            }
            catch (Exception ex)
            {
                content = "Not Connected";
            }

            Thread t = new Thread(new ThreadStart(delegate
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action<string>(SetValue), content);
            }
            ));
            t.Start();
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            isSynchronize();
        }
        private void SetTimer()
        {
            // Create a timer with a 60 second interval.
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static MagazineWebService2 GetWebClient(string uri = SERVICE_URL)
        {
            var client = new MagazineWebService2(new Uri(uri), new BasicAuthenticationCredentials());
            return client;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();
                var addPopUp = new AddPopUp();
                addPopUp.ShowDialog();

                client.Magazine.AddMagazineProduct(addPopUp.Name, addPopUp.Count, addPopUp.Localization);
                RefreshListOfEntires();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                foreach (MagazineEntryDisplay item in ListOfEntires.Items)
                {
                    if (item.Selected == true)
                    {
                        client.Magazine.RemoveMagazineProduct(item.Id.Value);
                    }
                }
                RefreshListOfEntires();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();

                foreach (MagazineEntryDisplay item in ListOfEntires.Items)
                {
                    if (item.Selected == true)
                    {
                        var modifyPopUp = new ModifyPopUp();
                        modifyPopUp.ShowDialog();

                        client.Magazine.ModifyMagazineProduct(item.Id.Value, modifyPopUp.Count);
                    }
                }
                RefreshListOfEntires();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private bool RefreshListOfEntires()
        {
            bool success = true;
            try
            {
                var client = GetWebClient();
                var list = client.Magazine.GetAllProducts();

                ListOfEntires.Items.Clear();

                foreach (var item in list)
                {
                    ListOfEntires.Items.Add(new MagazineEntryDisplay(item));
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }

        private void ShowError(string err)
        {
            var errorPopUp = new ErrorPopUp(err);
            errorPopUp.ShowDialog();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetWebClient();
                client.SynchronizationForUnit.Synchronize();
                if (RefreshListOfEntires() == false)
                {
                    throw new PrintDialogException();
                }
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
