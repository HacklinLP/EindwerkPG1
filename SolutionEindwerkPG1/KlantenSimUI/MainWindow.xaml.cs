using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Managers;
using KlantenSim_BL.Model;
using KlantenSim_Utils;
using Microsoft.Extensions.Configuration;
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
            _adresManager = new AdresManager(adresRepo);

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
}