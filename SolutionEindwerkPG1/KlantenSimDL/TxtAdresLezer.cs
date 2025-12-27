using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using KlantenSim_DL.DTOs;
using System.IO;

namespace KlantenSim_DL
{
    public class TxtAdresLezer : IAdresLezer
    {
        //public List<Adres> LeesAdressen(string pad)
        //{
        //    List<Gemeente> rawData = ReadFile(pad);

        //    return ConvertToAdres(rawData);

            
        //}

        //private List<Adres> ConvertToAdres(List<Gemeente> data)
        //{
        //    List<Adres> adressen = new();

        //    foreach (Gemeente dto in data)
        //    {
        //        Adres adres = new Adres(dto.Municipality, dto.Street, dto.HighwayType);
        //        adressen.Add(adres);
        //    }
        //    return adressen;
        //}

        public List<Gemeente> LeesAdressen(string pad)
        {
            List<Gemeente> gemeentes = new();

            string[] allowedTypes = { "residential" };

            Dictionary<string, Gemeente> gemeenteDict = new();

            int gemeenteId = 0;
            int straatId = 0;

            //string folderPath = Path.GetDirectoryName(pad);
            //string country = new DirectoryInfo(folderPath).Name;

            
            if (!File.Exists(pad))
            {
                return null;
            }

            
            using (StreamReader sr = new StreamReader(pad))
            {
                sr.ReadLine();
                string line;
                int linesread = 0;
                int streetsadded = 0;
                // lees tot het einde van de lijn/row
                while ((line = sr.ReadLine()) != null)
                {
                    linesread++;
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split(';');

                    string gemeenteNaam = parts[0].Trim();
                    string straatNaam = parts[1].Trim();
                    string highwayType = parts[2].Trim();

                    if (!allowedTypes.Contains(highwayType))
                    {
                        continue;
                    }

                    Gemeente huidigeLijnGemeente;
                    if (!gemeenteDict.ContainsKey(gemeenteNaam))
                    {
                        gemeenteId++;
                        huidigeLijnGemeente = new Gemeente(gemeenteId, gemeenteNaam);

                        huidigeLijnGemeente.Straten = new List<Straat>();

                        gemeentes.Add(huidigeLijnGemeente);
                        gemeenteDict.Add(gemeenteNaam, huidigeLijnGemeente);
                    }
                    else
                    {
                        huidigeLijnGemeente = gemeenteDict[gemeenteNaam];
                    }

                    straatId++;
                    Straat straat = new Straat(straatId, straatNaam, huidigeLijnGemeente.Id);
                    huidigeLijnGemeente.Straten.Add(straat);
                    streetsadded++;
                    /*if (country.Equals("Denemarken", StringComparison.OrdinalIgnoreCase))
                    {
                        // Rule: Remove "Kommune"
                        if (mun.EndsWith("Kommune", StringComparison.OrdinalIgnoreCase))
                        {
                            mun = mun.Substring(0, mun.Length - "Kommune".Length).Trim();
                        }
                    }

                    if (country.Equals("Zweden", StringComparison.OrdinalIgnoreCase))
                    {
                        // Rule: Remove "Kommun"
                        if (mun.EndsWith("kommun", StringComparison.OrdinalIgnoreCase))
                        {
                            mun = mun.Substring(0, mun.Length - "kommun".Length).Trim();
                        }
                    }

                    if (mun.Equals("(unknown)", StringComparison.OrdinalIgnoreCase))
                    {
                        mun = null;
                    }*/
                    //dtos.Add(new Gemeente(mun, street, htype));   
                }
                Console.WriteLine($"DEBUG: Read {linesread} lines. Added {streetsadded} streets.");
            }
            return gemeentes;
        }
    }
}
