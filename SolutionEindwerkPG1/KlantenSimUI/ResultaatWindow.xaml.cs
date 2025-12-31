using KlantenSim_BL.Model;
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

namespace KlantenSim_UI_WPF
{
    /// <summary>
    /// Interaction logic for ResultaatWindow.xaml
    /// </summary>
    public partial class ResultaatWindow : Window
    {
        public ResultaatWindow(List<SimulatieKlant> klanten)
        {
            InitializeComponent();

            dgKlanten.ItemsSource = klanten;
        }
    }
}
