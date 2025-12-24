using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using KlantenSim_DL.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_DL
{
    public class TxtNamenLezer : INaamLezer
    {

        public List<Naam> LeesNamen(string pad)
        {
            List<TxtNaamDTO> rawData = ReadFile(pad);

            return ConvertToNaam(rawData);


        }
        private List<Naam> ConvertToNaam(List<TxtNaamDTO> data)
        {
            List<Naam> namen = new();

            foreach (TxtNaamDTO dto in data)
            {
                Naam adres = new Naam(dto.Id, dto.Value, dto.Frequency);
                namen.Add(adres);
            }
            return namen;
        }

        private List<TxtNaamDTO> ReadFile(string pad)
        {
            List<TxtNaamDTO> dtos = new();

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
                if (country == "België")
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        string[] parts = line.Split(';');

                        int tempId;
                        int? ID = null;
                        if (int.TryParse(parts[0], out tempId))
                        {
                            // If it works, assign the value to your nullable ID
                            ID = tempId;
                        }

                        string Name = parts[1].Trim();

                        double tempFrequency;
                        double? Frequency = null;
                        if (double.TryParse(parts[2], out tempFrequency))
                        {
                            // If it works, assign the value to your nullable ID
                            Frequency = tempFrequency;
                        }
                        

                        dtos.Add(new TxtNaamDTO(ID, Name, Frequency));
                    }

                }

                if (country == "Denemarken")
                {
                    for (int i = 0;  i < 5; i++)
                    {
                        sr.ReadLine();
                    }
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        string Name = parts[0].Trim();

                        double tempFrequency;
                        double? Frequency = null;
                        if (double.TryParse(parts[1], out tempFrequency))
                        {
                            // If it works, assign the value to your nullable ID
                            Frequency = tempFrequency;
                        }


                        dtos.Add(new TxtNaamDTO(null, Name, Frequency));
                    }
                }

                if (country == "Finland")
                {
                    sr.ReadLine();

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        string Name = parts[0].Trim();

                        double tempFrequency;
                        double? Frequency = null;
                        if (double.TryParse(parts[1], out tempFrequency))
                        {
                            // If it works, assign the value to your nullable ID
                            Frequency = tempFrequency;
                        }


                        dtos.Add(new TxtNaamDTO(null, Name, Frequency));
                    }
                }
            }
            return dtos;

        }

    }
}
