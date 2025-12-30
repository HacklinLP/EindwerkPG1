using Microsoft.Extensions.Configuration;
using KlantenSim_BL.Config;
using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Managers
{
    public class AdresManager
    {
        private readonly Dictionary<string, LandConfig> _landenConfig;
        private readonly IAdresLezer _adresLezer;
        private readonly IAdresRepository _adresRepository;

        public AdresManager(Dictionary<string, LandConfig> configData, IAdresLezer adresLezer, IAdresRepository adresRepository)
        {
            _adresLezer = adresLezer;
            _landenConfig = configData;
            _adresRepository = adresRepository;
        }

        public AdresManager(IAdresRepository adresRepository)
        {
            _adresRepository = adresRepository;
        }

        public void VerwerkAlleAdressen(Dictionary<string, LandConfig> landen)
        {
            // Loop door elk land (België, Denemarken, etc.)
            foreach (var land in _landenConfig)
            {
                // Loop door elke versie (1, 2, etc.)
                foreach (var versie in land.Value.Versie)
                {
                    var instellingen = versie.Value.AdresInstellingen;
                    int huidigeVersieId = int.Parse(versie.Key);

                    if (instellingen != null && !string.IsNullOrEmpty(instellingen.Pad))
                    {
                        // Roep de reader aan met het juiste pad en de bijbehorende regels
                        List<Gemeente> gemeentes = _adresLezer.LeesAdressen(instellingen.Pad, instellingen, huidigeVersieId);

                        Console.WriteLine($"Klaar met lezen: {land.Key} (v{versie.Key}). Totaal aantal gemeentes gevonden: {gemeentes.Count}");

                        
                        _adresRepository.VoegAdresToe(land.Key.ToString(), versie.Key.ToString(), gemeentes);
                        
                    }
                }
            }
        }

        public List<string> GeefAlleLanden()
        {
            return _adresRepository.GeefAlleLanden();
        }
        public List<Gemeente> GeefGemeentesVoorLand(string landNaam)
        {
            // De manager roept de repository aan
            return _adresRepository.GeefGemeentesVoorLand(landNaam);
        }


    }
}
