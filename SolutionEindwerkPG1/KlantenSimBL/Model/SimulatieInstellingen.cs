using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class SimulatieInstellingen
    {
        public string Land { get; set; }
        public Dictionary<Gemeente, double> Gemeentes { get; set; }
        public int AantalKlanten { get; set; }

        public int MinLeeftijd { get; set; }
        public int MaxLeeftijd { get; set; }

        public string Opdrachtgever { get; set; }
        public int MaxHuisnummer { get; set; }
        public double PercentageMetLetter { get; set; }
    }
}
