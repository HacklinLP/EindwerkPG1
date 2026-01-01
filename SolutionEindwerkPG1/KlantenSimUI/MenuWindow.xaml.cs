using KlantenSim_BL.Managers;
using KlantenSimUI;
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
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private readonly AdresManager _adresManager;
        private readonly SimManager _simManager;

        public MenuWindow(AdresManager adresManager, SimManager simManager)
        {
            InitializeComponent();
            _adresManager = adresManager;
            _simManager = simManager;
        }

        private void btnSimMaken_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(_adresManager, _simManager);
            main.Show();
        }

        private void btnSimBekijken_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_SimExporteren_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
