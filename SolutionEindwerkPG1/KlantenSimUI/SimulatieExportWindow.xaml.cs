using KlantenSim_BL.Managers;
using KlantenSim_BL.Model;
using Microsoft.Win32;
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
    /// Interaction logic for SimulatieExportWindow.xaml
    /// </summary>
    public partial class SimulatieExportWindow : Window
    {
        private ExportManager _exportManager;
        private SimulatieInfo _simInfo;
        private SimulatieInstellingen _simInst;
        private List<SimulatieKlant> _simKlanten;
        private string pad;
        private string separator;
        public SimulatieExportWindow(ExportManager exportManager, SimulatieInfo simInfo, SimulatieInstellingen simInst, List<SimulatieKlant> simKlanten)
        {
            InitializeComponent();
            _exportManager = exportManager;
            _simInfo = simInfo;
            _simInst = simInst;
            _simKlanten = simKlanten;
        }
        private void cbExportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSeparator == null) return;

            // check of het een json file is - indien ja, geen separator
            if (cbExportType.SelectedIndex == 2)
                cbSeparator.Visibility = Visibility.Collapsed;
            else
                cbSeparator.Visibility = Visibility.Visible;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (cbExportType.SelectedIndex == 2) // als het een json file is
            {
                saveFileDialog.Filter = "JSON bestand (*.json)|*.json";
                saveFileDialog.DefaultExt = "json";
            }
            else // als het een txt is
            {
                saveFileDialog.Filter = "Tekstbestand (*.txt)|*.txt";
                saveFileDialog.DefaultExt = "txt";
            }

            // Als het pad is gekozen - volledige pad in textbox zetten ter info
            if (saveFileDialog.ShowDialog() == true)
            {
                // Zet het volledige pad in de TextBox
                txtExportPad.Text = saveFileDialog.FileName;
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            // 1. Checken of het formaat geselecteerd is
            if (cbExportType.SelectedItem == null)
            {
                
                MessageBox.Show("Selecteer eerst een export formaat.");
                return;
            }

            // 2. Checken of de separator ingevuld is indien nodig
            if (cbExportType.SelectedIndex != 2 && cbSeparator.SelectedItem == null)
            {
                MessageBox.Show("Selecteer a.u.b. een scheidingsteken voor het tekstbestand.", "Separator vereist", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 3. Checken of het pad wel degelijk ingevuld is
            if (string.IsNullOrWhiteSpace(txtExportPad.Text))
            {
                MessageBox.Show("Selecteer a.u.b. een locatie om het bestand op te slaan via de '...' knop.", "Pad vereist", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                pad = txtExportPad.Text;
            }
            if(cbExportType.SelectedIndex == 2)
            {
                separator = null;
            }
            else
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cbSeparator.SelectedItem;
                separator = selectedItem.Tag.ToString();
            }
            
           
            

            VoerExportUit(pad, separator);
        }

        private void VoerExportUit(string pad, string separator)
        {
            try
            {
                int keuze = cbExportType.SelectedIndex;

                if (keuze == 0) // 1 Tekstbestand
                {
                    _exportManager.ExporteerNaarTekst(_simInfo, _simInst, _simKlanten, pad, separator, true);
                }
                else if (keuze == 1) // 2 Tekstbestanden
                {
                    _exportManager.ExporteerNaarTekst(_simInfo, _simInst, _simKlanten, pad, separator, false);
                }
                else if (keuze == 2) // JSON
                {
                    _exportManager.ExporteerNaarJson(_simInfo, _simInst, _simKlanten, pad);
                }

                MessageBox.Show("Export succesvol opgeslagen op: " + pad);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een fout opgetreden bij het exporteren: " + ex.Message);
            }
        }

        

        
    }
}
