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
using System.Windows.Shapes;

namespace MagazineApp
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class AddPopUp : Window
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public string  Localization{ get; set; }
        public AddPopUp()
        {
            InitializeComponent();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Name = nameValue.Text;
            Count = Int32.Parse(countValue.Text);
            Localization = databaseValue.Text;
            Close();
        }
    }
}
