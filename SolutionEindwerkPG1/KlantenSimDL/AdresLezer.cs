using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using KlantenSim_DL.DTOs;

namespace KlantenSim_DL
{
    public class AdresLezer : IAdresLezer
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

            // check if file exists
            if (!File.Exists(pad))
            {
                return dtos;
            }

            // effectief lezen van het bestand
            using (StreamReader sr = new StreamReader(pad))
            {
                // skip first line (header)
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

                    AdresDTO dto = new AdresDTO
                    (
                        parts[0].Trim(), // municipality
                        parts[1].Trim(), // street
                        parts[2].Trim() // HighwayType
                    );

                    dtos.Add(dto);   
                }
            }
            return dtos;
        }
    }
}
