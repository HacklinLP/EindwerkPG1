using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class Achternaam
    {
        public Achternaam()
        {
        }

        public Achternaam(int iD, string naam, double? frequency, int versieId)
        {
            Id = iD;
            Naam = naam;
            Frequency = frequency;
            VersieId = versieId;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public double? Frequency { get; set; }
        public int VersieId { get; set; }
    }
}
