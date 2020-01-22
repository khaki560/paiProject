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
    /// Interaction logic for ErrorPopUp.xaml
    /// </summary>
    public partial class ErrorPopUp : Window
    {
        private string message;
        public ErrorPopUp(string err="")
        {
            message = err;
            InitializeComponent();
            if (this.message != "")
            {
                l.Content = message;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            this.Close();
        }
    }
}
