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
    /// Interaction logic for ResultaatWindow.xaml
    /// </summary>
    public partial class ResultaatWindow : Window
    {
        private readonly SimManager _simManager;
        private List<SimulatieKlant> _klanten;
        private readonly SimulatieInfo _info;
        private readonly SimulatieInstellingen _instellingen;
        private int _herbekijkSimId;
        
        public ResultaatWindow(SimManager simManager, List<SimulatieKlant> klanten, SimulatieInfo info, SimulatieInstellingen instellingen)
        {
            InitializeComponent();
            _simManager = simManager;
            _klanten = klanten;
            _info = info;
            _instellingen = instellingen;
            dgKlanten.ItemsSource = _klanten;

            btnOpslaan.Visibility = Visibility.Visible;
        }

        public ResultaatWindow(int simId, SimManager simManager )
        {
            InitializeComponent();
            _simManager = simManager;
            _herbekijkSimId = simId;
            btnOpslaan.Visibility = Visibility.Collapsed;

            _klanten = _simManager.HaalSimulatieKlantenOp(_herbekijkSimId);

            dgKlanten.ItemsSource = _klanten;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _simManager.BewaarSimulatie(_info, _instellingen, _klanten);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Foutmelding: {ex.Message}\n\nLocatie:\n{ex.StackTrace}");
            }
            MessageBox.Show("De simulatie is succesvol opgeslagen en geupload naar de databank!");
        }
    }
}
