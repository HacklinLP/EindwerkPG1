using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using KlantenSim_DL.DTOs;

namespace KlantenSim_DL
{
    public class CsvAdresLezer : IAdresLezer
    {
        public List<Adres> LeesAdressen(string pad)
        {
            List<AdresDTO> rawData = ReadFile(pad);

            return ConvertToAdres(rawData);

            
        }

        private List<Adres> ConvertToAdres(List<AdresDTO> data)
        {
            List<Adres> adressen = new();

            foreach (AdresDTO dto in data)
            {
                Adres adres = new Adres(dto.Municipality, dto.Street, dto.HighwayType);
                adressen.Add(adres);
            }
            return adressen;
        }

        private List<AdresDTO> ReadFile(string pad)
        {
            List<AdresDTO> dtos = new();

            string folderPath = Path.GetDirectoryName(pad);
            string country = new DirectoryInfo(folderPath).Name;

            // check if file exists
            if (!File.Exists(pad))
            {
                return dtos;
            }

            // effectief lezen van het bestand
            using (StreamReader sr = new StreamReader(pad))
            {
                sr.ReadLine();

                string line;

                // lees tot het einde van de lijn/row
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) // kleine check of er wel iets in staat
                    {
                        continue;
                    }

                    string[] parts = line.Split(';');

                    string mun = parts[0].Trim();
                    string street = parts[1].Trim();
                    string htype = parts[2].Trim();

                    
                    if (country.Equals("Denemarken", StringComparison.OrdinalIgnoreCase))
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

                    // (unknown) wegwerken uit de lijst
                    if (mun.Equals("(unknown)", StringComparison.OrdinalIgnoreCase))
                    {
                        mun = null;
                    }

                    dtos.Add(new AdresDTO(mun, street, htype));   
                }
            }
            return dtos;
        }
    }
}
