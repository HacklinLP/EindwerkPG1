using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Managers;
using KlantenSim_UI_WPF;
using KlantenSim_Utils;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace KlantenSimUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private AdresManager _adresManager;
    private SimManager _simManager;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            string connString = configuration.GetConnectionString("SQLserver");

            IAdresRepository adresRepo = BestandLezerFactory.MaakAdresRepository(connString);
            INaamRepository naamRepo = BestandLezerFactory.MaakNaamRepository(connString);
            ISimulatieRepository simRepo = BestandLezerFactory.MaakSimulatieRepository(connString);

            _adresManager = new AdresManager(adresRepo);
            _simManager = new SimManager(adresRepo, naamRepo, simRepo);

            // Start het MenuWindow en geef de managers mee via de constructor
            MenuWindow menu = new MenuWindow(_adresManager, _simManager);
            menu.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fout bij opstarten van het programma: {ex.Message}");
            Shutdown();
        }
    }
}

