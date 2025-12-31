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

            #region uploaden van data
            //adresManager.VerwerkAlleAdressen(landenConfig);
            //Console.WriteLine($"\n---- ADRESSEN UPLOADED ----\n");
            //naamManager.VerwerkAlleVoornamen(landenConfig);
            //Console.WriteLine($"\n---- VOORNAMEN UPLOADED ----\n");
            //naamManager.VerwerkAlleAchternamen(landenConfig);
            //Console.WriteLine($"\n---- ACHTERNAMEN UPLOADED ----\n");
            #endregion

            try
            {
                Console.WriteLine("--- START DATABASE TEST ---");

                // TEST 1: Land naar VersieID
                string testLand = "Belgie"; // Zorg dat dit land in je DB staat!
                Console.WriteLine($"\nStap 1: VersieID ophalen voor {testLand}...");
                int versieId = adresRepo.GeefVersieIdVoorLand(testLand);
                Console.WriteLine($"Gevonden VersieID: {versieId}");

                // TEST 2: Straten ophalen voor specifieke gemeentes
                // Vervang deze ID's door ID's die echt in je Gemeente-tabel staan
                List<int> testGemeenteIds = new List<int> { 1, 2, 3 };
                Console.WriteLine($"\nStap 2: Straten ophalen voor Gemeente IDs: {string.Join(",", testGemeenteIds)}...");

                var straten = adresRepo.GeefStratenVoorGemeentes(testGemeenteIds, versieId);

                Console.WriteLine($"Aantal straten gevonden: {straten.Count}");

                // Toon de eerste 5 straten als controle
                foreach (var straat in straten.Take(5))
                {
                    Console.WriteLine($"- {straat.Naam} (GemeenteID: {straat.GemeenteId})");
                }

                // TEST 3: Namen ophalen
                Console.WriteLine("\nStap 3: Namen ophalen voor VersieID...");
                var voornamen = naamRepo.GeefVoornamenVoorVersie(versieId);
                var achternamen = naamRepo.GeefAchternamenVoorVersie(versieId);

                Console.WriteLine($"Aantal voornamen gevonden: {voornamen.Count}");
                Console.WriteLine($"Aantal achternamen gevonden: {achternamen.Count}");

                // Controleer de frequentie van de eerste naam
                if (voornamen.Count > 0)
                {
                    var n = voornamen[0];
                    Console.WriteLine($"Check: Naam '{n.Naam}' heeft frequentie {n.Frequency}");
                }

                Console.WriteLine("\n--- TEST GESLAAGD ---");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n!!! TEST GEFAALD !!!");
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null) Console.WriteLine(ex.InnerException.Message);
            }

            Console.WriteLine("\nDruk op een toets om af te sluiten...");
            Console.ReadKey();
        }

    }    
}
