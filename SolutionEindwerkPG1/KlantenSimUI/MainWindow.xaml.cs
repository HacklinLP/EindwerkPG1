using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Managers;
using KlantenSim_BL.Model;
using KlantenSim_UI_WPF;
using KlantenSim_Utils;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KlantenSimUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private AdresManager _adresManager;
    private SimManager _simManager;
    private Dictionary<Gemeente, double> _gekozenGemeentes = new();

    public MainWindow()
    {
        InitializeComponent();

        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            string connString = configuration.GetConnectionString("SQLserver");

            IAdresRepository adresRepo = BestandLezerFactory.MaakAdresRepository(connString);
            INaamRepository naamRepo = BestandLezerFactory.MaakNaamRepository(connString);
            _adresManager = new AdresManager(adresRepo);
            _simManager = new SimManager(adresRepo, naamRepo);

            LaadLanden();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fout bij het opstarten: {ex.Message}");
        }
    }

    private void LaadLanden()
    {
        // Haal de lijst op uit de database via de manager
        List<string> landen = _adresManager.GeefAlleLanden();

        // Koppel de lijst aan de ComboBox
        cbLanden.ItemsSource = landen;
    }
    private void cbLanden_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbLanden.SelectedItem != null)
        {
            string geselecteerdLand = cbLanden.SelectedItem.ToString();

            // Haal de gemeentes op via de manager

            LaadGemeentes(geselecteerdLand);
            // Koppel de lijst aan de ListBox
            //lbGemeentes.ItemsSource = gemeentes;
        }
    }

    private void LaadGemeentes(string geselecteerdLand)
    {
        List<Gemeente> gemeentes = _adresManager.GeefGemeentesVoorLand(geselecteerdLand);

        // Koppel de lijst aan de ListBox
        lbGemeentes.ItemsSource = gemeentes;
    }

    private void CheckBox_Changed(object sender, RoutedEventArgs e)
    {
        var cb = (CheckBox)sender;
        var gemeente = (Gemeente)cb.Tag;

        if (cb.IsChecked == true)
        {
            if (!_gekozenGemeentes.ContainsKey(gemeente))
                _gekozenGemeentes.Add(gemeente, 0); // Start op 0%
        }
        else
        {
            _gekozenGemeentes.Remove(gemeente);
        }
    }

    private void TextBox_Percentage_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = (TextBox)sender;
        var gemeente = (Gemeente)tb.Tag;

        if (double.TryParse(tb.Text, out double percentage))
        {
            if (_gekozenGemeentes.ContainsKey(gemeente))
                _gekozenGemeentes[gemeente] = percentage;
        }
    }

    private void btnSimuleer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // 1. Doe hier de validatie of de gegevens wel correct zijn ingevuld allemaal
            if (cbLanden.SelectedItem == null) throw new Exception("Selecteer eerst een land");

            SimulatieInstellingen simInstelling = new SimulatieInstellingen
            {
                Land = cbLanden.SelectedItem.ToString(),
                Gemeentes = _gekozenGemeentes,
                AantalKlanten = int.Parse(txtAantalKlanten.Text),
                MinLeeftijd = int.Parse(txtMinLeeftijd.Text),
                MaxLeeftijd = int.Parse(txtMaxLeeftijd.Text),
                Opdrachtgever = txtOpdrachtgever.Text,
                MaxHuisnummer = int.Parse(txtMaxHuisnummer.Text),
                PercentageMetLetter = double.Parse(txtPercentageLetter.Text),
                
            };

            List<SimulatieKlant> resultaat = _simManager.StartSimulatie(simInstelling);

            ResultaatWindow resultaatWindow = new ResultaatWindow(resultaat);
            resultaatWindow.Show();

        }
        catch (FormatException)
        {
            MessageBox.Show("Zorg ervoor dat alle numerieke velden (leeftijd, aantal, huisnummer) correct zijn ingevuld.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}