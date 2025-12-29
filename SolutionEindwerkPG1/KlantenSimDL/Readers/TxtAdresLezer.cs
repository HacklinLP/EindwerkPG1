using KlantenSim_BL.Config;
using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using KlantenSim_DL.DTOs;
using System.IO;

namespace KlantenSim_DL.Readers
{
    public class TxtAdresLezer : IAdresLezer
    {
        public List<Gemeente> LeesAdressen(string pad, AdresInstellingen instellingen, int versieId)
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

                    foreach (string forbiddenWord in instellingen.VerbodenMunWoorden)
                    {
                        // We check and replace to handle cases like "Kommune" or "(unknown)"
                        gemeenteNaam = gemeenteNaam.Replace(forbiddenWord, "", StringComparison.OrdinalIgnoreCase);
                        gemeenteNaam = gemeenteNaam.Trim();
                    }

                    // 4. Grouping Logic: Check if we already have this Gemeente
                    if (!gemeenteDictionary.ContainsKey(gemeenteNaam))
                    {
                        var nieuweGemeente = new Gemeente(gemeenteIdCounter++, gemeenteNaam)
                        {
                            Straten = new List<Straat>(),
                            VersieId = versieId // You can set this later in the Manager
                        };
                        gemeenteDictionary.Add(gemeenteNaam, nieuweGemeente);
                    }

                    // 5. Add the street to the existing Gemeente
                    var straat = new Straat(straatIdCounter++, straatNaam, gemeenteDictionary[gemeenteNaam].Id);
                    gemeenteDictionary[gemeenteNaam].Straten.Add(straat);
                }
            }

            return gemeenteDictionary.Values.ToList();
        }
    }
}
