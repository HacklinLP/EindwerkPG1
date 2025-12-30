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

            string connString = config.GetConnectionString("SQLserver");

            IAdresLezer adresLezer = BestandLezerFactory.MaakTxtAdresLezer();
            INaamLezer naamLezer = BestandLezerFactory.MaakTxtNamenLezer();
            IAdresRepository adresRepo = BestandLezerFactory.MaakAdresRepository(connString);
            INaamRepository naamRepo = BestandLezerFactory.MaakNaamRepository(connString);

            AdresManager adresManager = new AdresManager(landenConfig, adresLezer, adresRepo);
            NaamManager naamManager = new NaamManager(landenConfig, naamLezer, naamRepo);


            adresManager.VerwerkAlleAdressen(landenConfig);
            Console.WriteLine($"\n---- ADRESSEN UPLOADED ----\n");
            naamManager.VerwerkAlleVoornamen(landenConfig);
            Console.WriteLine($"\n---- VOORNAMEN UPLOADED ----\n");
            naamManager.VerwerkAlleAchternamen(landenConfig);
            Console.WriteLine($"\n---- ACHTERNAMEN UPLOADED ----\n");

        }
    }
}
