using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KlantenSim_BL.Managers
{
    public class ExportManager
    {
        public void ExporteerNaarJson(SimulatieInfo info, SimulatieInstellingen inst, List<SimulatieKlant> klanten, string pad)
        {
            var leesbareGemeentes = inst.Gemeentes.ToDictionary(
                 kvp => kvp.Key.Naam, // gemeentenaam
                 kvp => kvp.Value     // percentage
    );
            // We maken een anoniem object om alles samen te voegen
            var exportObject = new
            {
                Statistieken = info,
                MetaInfo = new
                {
                    inst.Land,
                    inst.Opdrachtgever,
                    inst.AantalKlanten,
                    Gemeentes = leesbareGemeentes
                },
                Klanten = klanten
            };

            var options = new JsonSerializerOptions { WriteIndented = true }; // Voor mooie leesbare JSON
            string jsonString = JsonSerializer.Serialize(exportObject, options);

            File.WriteAllText(pad, jsonString);
        }

        public void ExporteerNaarTekst(SimulatieInfo info, SimulatieInstellingen inst, List<SimulatieKlant> klanten, string pad, string separator, bool singlebestand)
        {
            string infoPad = singlebestand ? pad : pad.Replace(".txt", "_info.txt");
            string dataPad = singlebestand ? pad : pad.Replace(".txt", "_data.txt");

            // Bij singlebestand gebruiken we Create, bij 2 bestanden overschrijven we de info-file
            using (StreamWriter sw = new StreamWriter(infoPad))
            {
                sw.WriteLine($"=== STATISTIEKEN (Simulatie ID: {info.Id}) ===");
                sw.WriteLine($"Datum{separator}{info.AanmaakDatum:dd/MM/yyyy HH:mm}");
                sw.WriteLine($"Aantal klanten aangemaakt{separator}{info.AantalKlantenAangemaakt}");
                sw.WriteLine($"Gemiddelde leeftijd{separator}{info.GemiddeldeLeeftijd}");
                sw.WriteLine($"Leeftijdsbereik{separator}{info.JongsteLeeftijd} - {info.OudsteLeeftijd}");
                sw.WriteLine($"{info.Id}{separator}{info.AanmaakDatum:dd/MM/yyyy HH:mm}{separator}{info.AantalKlantenAangemaakt}{separator}{info.GemiddeldeLeeftijd}{separator}{info.JongsteLeeftijd} - {info.OudsteLeeftijd}");

                sw.WriteLine("");
                sw.WriteLine($"=== META-INFO (Instellingen) ===");
                sw.WriteLine($"Opdrachtgever{separator}{inst.Opdrachtgever}");
                sw.WriteLine($"Land{separator}{inst.Land}");
                sw.WriteLine($"Max Huisnummer{separator}{inst.MaxHuisnummer}");
                sw.WriteLine($"Percentage met letter{separator}{inst.PercentageMetLetter}%");

                sw.WriteLine("");
            }

            // 2. Schrijf de Klantgegevens
            // Bij singlebestand gebruiken we 'Append: true' om onder de info verder te schrijven
            // Bij 2 bestanden maken we een nieuw bestand aan voor de data
            using (StreamWriter sw = new StreamWriter(dataPad, singlebestand))
            {
                sw.WriteLine("=== EFFECTIEVE GEGEVENS ===");
                // Header voor de kolommen
                sw.WriteLine($"Voornaam{separator}Achternaam{separator}Gender{separator}Gemeente{separator}Straat{separator}Huisnummer{separator}Geboortedatum");

                foreach (var k in klanten)
                {
                    sw.WriteLine($"{k.Voornaam}{separator}{k.Achternaam}{separator}{k.Gender}{separator}{k.Gemeente}{separator}{k.Straat}{separator}{k.Huisnummer}{separator}{k.Geboortedatum:dd/MM/yyyy}");
                }
            }
        }
    }
}
