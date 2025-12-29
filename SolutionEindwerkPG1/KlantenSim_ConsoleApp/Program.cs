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
            INaamLezer naamLezer = BestandLezerFactory.MaakTxtNamenLezer();

            AdresManager adresManager = new AdresManager(landenConfig, adresLezer);
            NaamManager naamManager = new NaamManager(landenConfig, naamLezer);

            adresManager.VerwerkAlleAdressen(landenConfig);

            //naamManager.VerwerkAlleVoornamen(landenConfig);
            //naamManager.VerwerkAlleAchternamen(landenConfig);

        }
    }
}
