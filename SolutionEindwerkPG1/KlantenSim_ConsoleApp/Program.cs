using KlantenSim_BL.Config;
using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Managers;
using KlantenSim_Utils;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace KlantenSim_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var landenConfig = config.GetSection("Landen").Get<Dictionary<string, LandConfig>>();
            // This maps the "Landen" section to a Dictionary<string, LandConfig>



            IAdresLezer adresLezer = BestandLezerFactory.MaakTxtAdresLezer();
            INaamLezer namenLezer = BestandLezerFactory.MaakTxtNamenLezer();

            AdresManager manager = new AdresManager(landenConfig, adresLezer);

            manager.VerwerkAlleAdressen(landenConfig);
            
            //manager.StartTestNaam(@"C:\Users\lucas\Documents\HoGent\Eindwerk_PG1\EindwerkPG1\Data\Denemarken\efternavne 2025 (3+) - med overskrifter.txt");
            //manager.StartTestNaam(@"C:\Users\lucas\Documents\HoGent\Eindwerk_PG1\EindwerkPG1\Data\Finland\etunimitilasto-2025-08-13-dvv_miehet_ens.txt");
        }
    }
}
