using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class Voornaam
    {
        public Voornaam(int? iD, string naam, string gender, double? frequency, int versieid)
        {
            ID = iD;
            Naam = naam;
            Gender = gender;
            Frequency = frequency;
            VersieId = versieid;
        }

        public int? ID { get; set; }
        public string Naam { get; set; }

        public string Gender { get; set; }
        public double? Frequency { get; set; }

        public int VersieId { get; set; }

    }
}
