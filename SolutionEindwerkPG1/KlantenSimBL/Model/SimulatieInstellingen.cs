using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class SimulatieInstellingen
    {
        public SimulatieInstellingen()
        {
            
        }
        public SimulatieInstellingen(string land, Dictionary<Gemeente, double> gemeentes, int aantalKlanten, int minLeeftijd, int maxLeeftijd, string opdrachtgever, int maxHuisnummer, double percentageMetLetter)
        {
            Land = land;
            Gemeentes = gemeentes;
            AantalKlanten = aantalKlanten;
            MinLeeftijd = minLeeftijd;
            MaxLeeftijd = maxLeeftijd;
            Opdrachtgever = opdrachtgever;
            MaxHuisnummer = maxHuisnummer;
            PercentageMetLetter = percentageMetLetter;
        }

        public int ID { get; set; }
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
