using KlantenSim_BL.Config;
using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using KlantenSim_DL.DTOs;
using System.IO;

namespace KlantenSim_DL
{
    public class TxtAdresLezer : IAdresLezer
    {
        //public void ProcessAllFiles(Dictionary<string, LandConfig> landen)
        //{
        //    foreach (var landKvp in landen)
        //    {
        //        string landNaam = landKvp.Key; // e.g., "Belgie"

        //        foreach (var versieKvp in landKvp.Value.Versie)
        //        {
        //            string versieNummer = versieKvp.Key; // e.g., "1"
        //            var instellingen = versieKvp.Value;

        //            // 1. Get the Address Settings for your logic
        //            var adresInstellingen = instellingen.AdresInstellingen;

        //            // 2. Get the File Paths from the Name Settings
        //            foreach (var file in instellingen.VoornaamInstellingen.Bestanden)
        //            {
        //                string pad = file.Pad;
        //                Console.WriteLine($"Reading {landNaam} v{versieNummer}: {pad}");

        //                // Now call your reader
        //                List<Gemeente> result = LeesAdressen(pad, adresInstellingen);
        //            }
        //        }
        //    }
        //}

        public List<Gemeente> LeesAdressen(string pad, AdresInstellingen instellingen)
        {
            // Use a dictionary to group streets by municipality name
            Dictionary<string, Gemeente> gemeenteDictionary = new();
            int gemeenteIdCounter = 0;
            int straatIdCounter = 0;

            using (StreamReader reader = new StreamReader(pad))
            {
                // 1. Skip lines (headers)
                for (int i = 0; i < instellingen.SkipLines; i++)
                {
                    reader.ReadLine();
                }

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue; // in geval van lege lijn
                    }

                    string[] columns = line.Split(instellingen.Separator);

                    // 2. Extract data using indices from JSON
                    string gemeenteNaam = columns[instellingen.MunIndex].Trim();
                    string straatNaam = columns[instellingen.StraatIndex].Trim();
                    string highwayType = columns[instellingen.HighwayTypeIndex].Trim();

                    // 3. Apply Filters
                    if (!instellingen.ToegestaneMun.Contains(highwayType)) continue;

                    bool isForbidden = instellingen.VerbodenMunWoorden.Any(w => gemeenteNaam.Contains(w, StringComparison.OrdinalIgnoreCase));
                    if (isForbidden) continue;

                    // 4. Grouping Logic: Check if we already have this Gemeente
                    if (!gemeenteDictionary.ContainsKey(gemeenteNaam))
                    {
                        var nieuweGemeente = new Gemeente(gemeenteIdCounter++, gemeenteNaam)
                        {
                            Straten = new List<Straat>(),
                            VersieId = 0 // You can set this later in the Manager
                        };
                        gemeenteDictionary.Add(gemeenteNaam, nieuweGemeente);
                    }

                    // 5. Add the street to the existing Gemeente
                    var straat = new Straat(straatIdCounter++, straatNaam, gemeenteDictionary[gemeenteNaam].Id);
                    gemeenteDictionary[gemeenteNaam].Straten.Add(straat);
                }
            }

            return gemeenteDictionary.Values.ToList();
            

        //    //string[] allowedTypes = { "residential" };

        //    Dictionary<string, Gemeente> gemeenteDict = new();

        //    int gemeenteId = 0;
        //    int straatId = 0;

        //    //string folderPath = Path.GetDirectoryName(pad);
        //    //string country = new DirectoryInfo(folderPath).Name;

            
        //    if (!File.Exists(pad))
        //    {
        //        return null;
        //    }


        //    using (StreamReader sr = new StreamReader(pad))
        //    {
        //        sr.ReadLine();
        //        string line;
        //        int linesread = 0;
        //        int streetsadded = 0;
        //        // lees tot het einde van de lijn/row
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            linesread++;
        //            if (string.IsNullOrWhiteSpace(line))
        //            {
        //                continue;
        //            }

        //            string[] parts = line.Split(';');

        //            string gemeenteNaam = parts[0].Trim();
        //            string straatNaam = parts[1].Trim();
        //            string highwayType = parts[2].Trim();

        //            if (!allowedTypes.Contains(highwayType))
        //            {
        //                continue;
        //            }

        //            Gemeente huidigeLijnGemeente;
        //            if (!gemeenteDict.ContainsKey(gemeenteNaam))
        //            {
        //                gemeenteId++;
        //                huidigeLijnGemeente = new Gemeente(gemeenteId, gemeenteNaam);

        //                huidigeLijnGemeente.Straten = new List<Straat>();

        //                gemeentes.Add(huidigeLijnGemeente);
        //                gemeenteDict.Add(gemeenteNaam, huidigeLijnGemeente);
        //            }
        //            else
        //            {
        //                huidigeLijnGemeente = gemeenteDict[gemeenteNaam];
        //            }

        //            straatId++;
        //            Straat straat = new Straat(straatId, straatNaam, huidigeLijnGemeente.Id);
        //            huidigeLijnGemeente.Straten.Add(straat);
        //            streetsadded++;
        //        }
        //        Console.WriteLine($"DEBUG: Read {linesread} lines. Added {streetsadded} streets.");
        //    }
        //    return gemeentes;
        }
    }
}
