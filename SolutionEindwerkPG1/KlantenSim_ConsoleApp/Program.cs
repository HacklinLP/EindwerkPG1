using KlantenSim_BL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KlantenSim_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            string? connectionString = config.GetConnectionString("SQLserver");
            string? folder = config.GetSection("AppSettings")["folder"];
            string? bestandsnaam = config.GetSection("AppSettings")["ProvincieIDsVlaanderen"];

            if (string.IsNullOrEmpty(bestandsnaam))
            {
                Console.WriteLine("Bestandsnaam is not configured.");
                return;
            }

            // You must instantiate adresLezer with a concrete implementation.
            // Replace 'ConcreteAdresLezer' with the actual class that implements IAdresLezer.
            

            var output = adresLezer.LeesAdressen(bestandsnaam);
        }
    }
}
