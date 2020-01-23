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
    public partial class EditUnitsPopUp : Window
    {
        public IList<Tuple<string, string>> units { get; set; }
        public EditUnitsPopUp(IList<Tuple<string, string>> units)
        {
            InitializeComponent();
            this.units = units;

            foreach(var unit in this.units)
            {
                unitsCombo.Items.Add(unit.Item1 + ", url: " + unit.Item2);
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            this.units.RemoveAt(unitsCombo.SelectedIndex);
            Close();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            string name = nameValue.Text;
            string hostAndPort = hostAndPortValue.Text;
            this.units.Add(new Tuple<string, string>(name, hostAndPort));
            Close();
        }
    }
}
