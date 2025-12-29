using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class Achternaam
    {
        public Achternaam(int iD, string naam, double? frequency, int versieId)
        {
            ID = iD;
            Naam = naam;
            Frequency = frequency;
            VersieId = versieId;
        }

        public int ID { get; set; }
        public string Naam { get; set; }
        public double? Frequency { get; set; }
        public int VersieId { get; set; }
    }
}
