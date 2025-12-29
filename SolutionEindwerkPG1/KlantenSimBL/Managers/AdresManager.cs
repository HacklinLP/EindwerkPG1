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

        public AdresManager(Dictionary<string, LandConfig> configData, IAdresLezer adresLezer)
        {
            _adresLezer = adresLezer;
            _landenConfig = configData;
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


                        _adresRepository.VoegAdresToe(land.Key.ToString(), int.Parse(versie.Key.ToString()), gemeentes);
                        
                    }
                }
            }
        }
        // dit mag weg na het testen
        //public void StartTestAdres(string pad)
        //{
        //    //adres
        //    List<Gemeente> alleAdressen = _adresLezer.LeesAdressen(pad, adresInstellingen);

        //    Console.WriteLine($"{alleAdressen.Count()} ADRESSEN GELADEN");

        //    int adresCounter = 0;
        //    foreach (Gemeente g in alleAdressen)
        //    {
        //        Console.WriteLine($"Found: {g.Id}, {g.Naam}, {g.Straten.Count()}");
        //        adresCounter++;

        //        if (adresCounter == 5) break;
        //    }
        //}

        /*public void StartTestNaam(string pad)
        {
            //naam
            List<Naam> alleNamen = _namenLezer.LeesNamen(pad);

            Console.WriteLine($"\n{alleNamen.Count()} NAMEN GELADEN");

            int namenCounter = 0;
            foreach (Naam n in alleNamen)
            {
                Console.WriteLine($"Found: {n.ID}, {n.NaamValue}, {n.Frequency}");
                namenCounter++;

                if (namenCounter == 5) break;
            }
        }*/


    }
}
