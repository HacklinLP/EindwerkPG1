using KlantenSim_BL.Config;
using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Managers
{
    public class NaamManager
    {
        private readonly Dictionary<string, LandConfig> _landenConfig;
        private readonly INaamLezer _naamLezer;

        public NaamManager(Dictionary<string, LandConfig> configData, INaamLezer naamLezer)
        {
            _naamLezer = naamLezer;
            _landenConfig = configData;
        }

        public void VerwerkAlleVoornamen(Dictionary<string, LandConfig> landen)
        {
            // Loop door elk land (België, Denemarken, etc.)
            foreach (var land in _landenConfig)
            {
                // Loop door elke versie (1, 2, etc.)
                foreach (var versie in land.Value.Versie)
                {
                    var instellingen = versie.Value.VoornaamInstellingen;
                    int huidigeVersieId = int.Parse(versie.Key);

                    if (instellingen != null && instellingen.Bestanden != null)
                    {
                        foreach (var bestand in instellingen.Bestanden)
                        {
                            Console.WriteLine($"Bezig met lezen: {land.Key} ({bestand.Gender}) - {bestand.Pad}");

                            // 3. Call your name reader (you'll likely pass the specific file settings)
                            // Note: You pass 'bestand' for the path/gender and 'instellingen' for the indices/separator
                            var namen = _naamLezer.LeesVoornamen(bestand, instellingen, huidigeVersieId);

                            Console.WriteLine($"Klaar! {namen.Count} voornamen gevonden voor {land.Key} ({bestand.Gender}).");
                            for (int i = 0; i < 10; i++)
                            {
                                Console.WriteLine($"{namen[i].Naam} | {namen[i].Gender}");
                            }
                        }
                        
                    }
                }
            }
        }

        public void VerwerkAlleAchternamen(Dictionary<string, LandConfig> landen)
        {
            // Loop door elk land (België, Denemarken, etc.)
            foreach (var land in _landenConfig)
            {
                // Loop door elke versie (1, 2, etc.)
                foreach (var versie in land.Value.Versie)
                {
                    var instellingen = versie.Value.AchternaamInstellingen;
                    int huidigeVersieId = int.Parse(versie.Key);

                    if (instellingen != null && instellingen.Bestanden != null)
                    {
                        foreach (var bestand in instellingen.Bestanden)
                        {
                            Console.WriteLine($"Bezig met lezen: {land.Key} ({bestand.Gender}) - {bestand.Pad}");

                            // 3. Call your name reader (you'll likely pass the specific file settings)
                            // Note: You pass 'bestand' for the path/gender and 'instellingen' for the indices/separator
                            var namen = _naamLezer.LeesAchternamen(bestand, instellingen, huidigeVersieId);

                            Console.WriteLine($"Klaar! {namen.Count} achternamen gevonden voor {land.Key}");
                            for (int i = 0; i < 10; i++)
                            {
                                Console.WriteLine($"{namen[i].Naam} | {namen[i].Frequency}");
                            }
                        }

                    }
                }
            }
        }
    }
}
