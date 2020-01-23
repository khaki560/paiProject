using MagazineApp.Models;
using Microsoft.Rest;
using System;
using System.Collections;
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
        private IList<Tuple<string, string>> unitsToSync { get; set; }
        private string filter = "All";

        public MainWindow()
        {
            unitsToSync = new List<Tuple<string, string>>();

            InitializeComponent();
            RefreshListOfEntires();
            isSynchronize();
            SetTimer();
            CreateFilter();
        }

        private void SetFilterValue(string txt)
        {

        }

        private void FilterLabelClick(object sender, RoutedEventArgs e)
        {
            Label button = sender as Label;
            Filter.Content = button.Content;
            filter = button.Content as string;
            RefreshListOfEntires(filter);
        }


        private void CreateFilter()
        {
            try
            {
                var client = GetWebClient();
                var c = client.SynchronizationForUnit.GetLocations();

                string[] a = ((IEnumerable)c).Cast<object>()
                                 .Select(x => x.ToString())
                                 .ToArray();

                foreach (var _a in a)
                {
                    var b = new Label { Content = _a };
                    b.MouseLeftButtonUp += FilterLabelClick;
                    Filter.ContextMenu.Items.Add(b);
                }
            }
            catch (Exception ex)
            {
            }

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

        private void ButttonEditUnits_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editUnits = new EditUnitsPopUp(unitsToSync);
                editUnits.ShowDialog();

                unitsToSync = editUnits.units;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
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

        private bool RefreshListOfEntires(string filtr="All")
        {
            bool success = true;
            try
            {
                var client = GetWebClient();
                var list = client.Magazine.GetAllProducts();

                ListOfEntires.Items.Clear();

                foreach (var item in list)
                {
                    if(filtr == "All")
                    {
                        ListOfEntires.Items.Add(new MagazineEntryDisplay(item));
                    }
                    else
                    {
                        if(item.Localization == filtr)
                        {
                            ListOfEntires.Items.Add(new MagazineEntryDisplay(item));
                        }
                    }
                    
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
