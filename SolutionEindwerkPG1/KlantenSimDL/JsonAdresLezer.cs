using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using KlantenSim_DL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KlantenSim_DL
{
    public class JsonAdresLezer : IAdresLezer
    {
        public List<Adres> LeesAdressen(string pad)
        {
            
        }
        public (List<MunicipalityDTO>, List<StraatDTO>) ReadJsonAddresses(string pad)
        {
            List<MunicipalityDTO> muniDtos = new();
            List<StraatDTO> streetDtos = new();

            if (!File.Exists(pad)) return (muniDtos, streetDtos);

            // 1. Read and Deserialize
            string jsonString = File.ReadAllText(pad);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dataSource = JsonSerializer.Deserialize<JsonAddressSource>(jsonString, options);

            if (dataSource?.Address != null)
            {
                // 2. Map city_name array to MunicipalityDTOs
                foreach (var name in dataSource.Address.CityNames)
                {
                    muniDtos.Add(new MunicipalityDTO(name));
                }

                // 3. Map street array to StraatDTOs
                foreach (var name in dataSource.Address.Street)
                {
                    streetDtos.Add(new StraatDTO(name));
                }
            }

            return (muniDtos, streetDtos);
        }
}
