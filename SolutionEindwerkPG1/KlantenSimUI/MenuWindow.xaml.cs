using KlantenSim_BL.Managers;
using KlantenSim_BL.Model;
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
        private readonly ExportManager _exportManager;

        public MenuWindow(AdresManager adresManager, SimManager simManager, ExportManager exportManager)
        {
            InitializeComponent();
            _adresManager = adresManager;
            _simManager = simManager;
            _exportManager = exportManager;
        }

        private void btnSimMaken_Click(object sender, RoutedEventArgs e)
        {
            SimulatorMaken main = new SimulatorMaken(_adresManager, _simManager);
            main.Show();
        }

        private void btnSimBekijken_Click(object sender, RoutedEventArgs e)
        {
            SimulatieBekijkenWindow simulatieBekijkenWindow = new SimulatieBekijkenWindow(_adresManager, _simManager, _exportManager);
            simulatieBekijkenWindow.Show();
        }

        //private void btn_SimExporteren_Click(object sender, RoutedEventArgs e)
        //{
        //    SimulatieExportWindow simulatieExportWindow = new SimulatieExportWindow(_exportManager);
        //    simulatieExportWindow.Show();
        //}

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
