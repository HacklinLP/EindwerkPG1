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

                    if (instellingen != null && !string.IsNullOrEmpty(instellingen.Pad))
                    {
                        // Roep de reader aan met het juiste pad en de bijbehorende regels
                        List<Gemeente> gemeentes = _adresLezer.LeesAdressen(instellingen.Pad, instellingen);

                        // --- Testing Output ---
                        Console.WriteLine($"\n[TEST] Gevonden resultaten voor pad: {instellingen.Pad}");
                        Console.WriteLine($"Totaal aantal gemeentes: {gemeentes.Count}");

                        foreach (var g in gemeentes)
                        {
                            Console.WriteLine($"\nGemeente: {g.Naam} (ID: {g.Id})");
                            Console.WriteLine($"Aantal straten: {g.Straten.Count}");

                            // Print the first 5 streets as a sample to avoid flooding the console
                            var streetSample = g.Straten.Take(5).ToList();
                            foreach (var s in streetSample)
                            {
                                Console.WriteLine($"  - Straat: {s.Naam} (G_ID: {s.GemeenteId})");
                            }

                            if (g.Straten.Count > 5)
                            {
                                Console.WriteLine($"  ... en nog {g.Straten.Count - 5} andere straten.");
                            }
                        }
                        Console.WriteLine("\n--------------------------------------------------\n");

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
