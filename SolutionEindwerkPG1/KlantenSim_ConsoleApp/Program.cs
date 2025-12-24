using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Managers;
using KlantenSim_Utils;
using Microsoft.Extensions.Configuration;

namespace KlantenSim_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //var config = builder.Build();

            //string? connectionString = config.GetConnectionString("SQLserver");
            //string? folder = config.GetSection("AppSettings")["folder"];
            //string? bestandsnaam = config.GetSection("AppSettings")["ProvincieIDsVlaanderen"];

            //if (string.IsNullOrEmpty(bestandsnaam))
            //{
            //    Console.WriteLine("Bestandsnaam is not configured.");
            //    return;
            //}

            IAdresLezer adresLezer = BestandLezerFactory.MaakTxtAdresLezer();
            INaamLezer namenLezer = BestandLezerFactory.MaakTxtNamenLezer();

            BestandManager manager = new BestandManager(adresLezer, namenLezer);
            

            manager.StartTestAdres(@"C:\Users\lucas\Documents\HoGent\Eindwerk_PG1\EindwerkPG1\Data\Zweden\sweden_streets2.csv");
            manager.StartTestNaam(@"C:\Users\lucas\Documents\HoGent\Eindwerk_PG1\EindwerkPG1\Data\België\Familienamen_2024_Belgie.csv");

        }
    }
}
