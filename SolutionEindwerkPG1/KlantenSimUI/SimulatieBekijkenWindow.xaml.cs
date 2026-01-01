using KlantenSim_BL.Managers;
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
    /// Interaction logic for SimulatieBekijken.xaml
    /// </summary>
    public partial class SimulatieBekijkenWindow : Window
    {
        private readonly AdresManager _adresManager;
        private readonly SimManager _simManager;
        public SimulatieBekijkenWindow(AdresManager adresManager, SimManager simManager)
        {
            InitializeComponent();
            _adresManager = adresManager;
            _simManager = simManager;

            List<SimulatieInfo> simulatieInfos = _simManager.HaalSimulatieInfoOp();

            dgSimInfo.ItemsSource = simulatieInfos;
        }

        private void MenuItem_BekijkSimulatie(object sender, RoutedEventArgs e)
        {
            if (dgSimInfo.SelectedItem is SimulatieInfo geselecteerdeSim)
            {
                // Optie A: Je opent je bestaande ResultaatWindow
                // We moeten dan wel zorgen dat dit window de data kan inladen op basis van de ID
                ResultaatWindow detailWindow = new ResultaatWindow(geselecteerdeSim.Id, _simManager);
                detailWindow.Show();
            }
        }
    }
}
