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
    /// Interaction logic for ModifyPopUp.xaml
    /// </summary>
    public partial class ModifyPopUp : Window
    {

        public int Count { set; get; }
        public double Price { set; get; }
        public ModifyPopUp()
        {
            InitializeComponent();
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            Count = Int32.Parse(countValue.Text);
            Price = Double.Parse(priceValue.Text);
            Close();
        }
    }
}
