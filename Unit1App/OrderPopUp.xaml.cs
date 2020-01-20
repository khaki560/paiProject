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

namespace Unit1App
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderPopUp : Window
    {

        public string  Count { get; set; }
        public OrderPopUp()
        {
            InitializeComponent();
        }

        private void OrderAdd(object sender, RoutedEventArgs e)
        {
            Count = CountValue.Text;
            Close();
        }
    }
}
