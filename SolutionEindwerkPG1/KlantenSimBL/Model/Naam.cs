using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class Naam
    {
        public Naam(int? iD, string naam, double? frequency)
        {
            ID = iD;
            NaamValue = naam;
            Frequency = frequency;
        }

        public int? ID { get; set; }
        public string NaamValue { get; set; }
        public double? Frequency { get; set; }
    }
}
