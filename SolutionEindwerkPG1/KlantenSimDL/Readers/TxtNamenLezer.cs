using KlantenSim_BL.Config;
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
using System.Xml.Linq;

namespace KlantenSim_DL.Readers
{
    public class TxtNamenLezer : INaamLezer
    {

        public List<Voornaam> LeesVoornamen(BestandenConfig bestand, VoornaamInstellingen instellingen, int versieId)
        {
            List<Voornaam> voornamen = new();
            int voornaamIdCounter = 1;

            int frequencyIndex = bestand.FrequencyIndex ?? instellingen.FrequencyIndex;

            using(StreamReader reader = new StreamReader(bestand.Pad))
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
                    string voornaam = columns[instellingen.NaamIndex].Trim();
                    string frequency = columns[frequencyIndex].Trim();
                    string gender = bestand.Gender;

                    // DETECTION: If the name is empty or the line is a footer line
                    if (string.IsNullOrEmpty(voornaam) || voornaam.StartsWith("*"))
                    {
                        break; // Stop reading the file entirely
                    }

                    double actualFrequency = 0;
                    string cleanFreq = new string(frequency.Where(char.IsDigit).ToArray());
                    double.TryParse(cleanFreq, out actualFrequency);

                    voornamen.Add(new Voornaam(voornaamIdCounter, voornaam, gender, actualFrequency, versieId));
                }
            }
            return voornamen;
        }

        public List<Achternaam> LeesAchternamen(BestandenConfig bestand, AchternaamInstellingen instellingen, int versieId)
        {
            List<Achternaam> achternamen = new();
            int achternaamIdCounter = 1;

            int frequencyIndex = bestand.FrequencyIndex ?? instellingen.FrequencyIndex;

            using (StreamReader reader = new StreamReader(bestand.Pad))
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
                    string achternaam = columns[instellingen.NaamIndex].Trim();
                    string frequency = columns[frequencyIndex].Trim();

                    if (string.IsNullOrEmpty(achternaam) || achternaam.StartsWith("Stand der"))
                    {
                        break; // Stop reading the file entirely
                    }

                    double actualFrequency = 0;
                    string cleanFreq = new string(frequency.Where(char.IsDigit).ToArray());
                    double.TryParse(cleanFreq, out actualFrequency);

                    achternamen.Add(new Achternaam(achternaamIdCounter, achternaam, actualFrequency, versieId));
                }
            }
            return achternamen;
        }



        //public List<Voornaam> LeesNamen(string pad)
        //{
        //    List<TxtNaamDTO> rawData = ReadFile(pad);

        //    return ConvertToNaam(rawData);


        //}
        //private List<Voornaam> ConvertToNaam(List<TxtNaamDTO> data)
        //{
        //    List<Voornaam> namen = new();

        //    foreach (TxtNaamDTO dto in data)
        //    {
        //        Voornaam adres = new Voornaam(dto.Id, dto.Value, dto.Frequency);
        //        namen.Add(adres);
        //    }
        //    return namen;
        //}

        //private List<TxtNaamDTO> ReadFile(string pad)
        //{
        //    List<TxtNaamDTO> dtos = new();

        //    string folderPath = Path.GetDirectoryName(pad);
        //    string country = new DirectoryInfo(folderPath).Name;

        //    // check if file exists
        //    if (!File.Exists(pad))
        //    {
        //        return dtos;
        //    }

        //    // effectief lezen van het bestand
        //    using (StreamReader sr = new StreamReader(pad))
        //    {
        //        if (country == "België")
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                if (string.IsNullOrWhiteSpace(line))
        //                {
        //                    continue;
        //                }

        //                string[] parts = line.Split(';');

        //                int tempId;
        //                int? ID = null;
        //                if (int.TryParse(parts[0], out tempId))
        //                {
        //                    // If it works, assign the value to your nullable ID
        //                    ID = tempId;
        //                }

        //                string Name = parts[1].Trim();

        //                double tempFrequency;
        //                double? Frequency = null;
        //                if (double.TryParse(parts[2], out tempFrequency))
        //                {
        //                    // If it works, assign the value to your nullable ID
        //                    Frequency = tempFrequency;
        //                }


        //                dtos.Add(new TxtNaamDTO(ID, Name, Frequency));
        //            }

        //        }

        //        if (country == "Denemarken")
        //        {
        //            for (int i = 0;  i < 5; i++)
        //            {
        //                sr.ReadLine();
        //            }
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                if (string.IsNullOrWhiteSpace(line))
        //                {
        //                    continue;
        //                }

        //                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        //                string Name = parts[0].Trim();

        //                double tempFrequency;
        //                double? Frequency = null;
        //                if (double.TryParse(parts[1], out tempFrequency))
        //                {
        //                    // If it works, assign the value to your nullable ID
        //                    Frequency = tempFrequency;
        //                }


        //                dtos.Add(new TxtNaamDTO(null, Name, Frequency));
        //            }
        //        }

        //        if (country == "Finland")
        //        {
        //            sr.ReadLine();

        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                if (string.IsNullOrWhiteSpace(line))
        //                {
        //                    continue;
        //                }

        //                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        //                string Name = parts[0].Trim();

        //                double tempFrequency;
        //                double? Frequency = null;
        //                if (double.TryParse(parts[1], out tempFrequency))
        //                {
        //                    // If it works, assign the value to your nullable ID
        //                    Frequency = tempFrequency;
        //                }


        //                dtos.Add(new TxtNaamDTO(null, Name, Frequency));
        //            }
        //        }
        //    }
        //    return dtos;

        //}

    }
}
