using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class SimulatieKlant
    {
        public SimulatieKlant()
        {
        }

        public SimulatieKlant(int id, string voornaam, string achternaam, string gender, string gemeente, string straat, string huisnummer, DateTime geboortedatum, int brondataId)
        {
            Id = id;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Gender = gender;
            Gemeente = gemeente;
            Straat = straat;
            Huisnummer = huisnummer;
            Geboortedatum = geboortedatum;
            BrondataId = brondataId;
        }

        public int Id { get; set; }
        public string Voornaam { get; set; }
        
        public string Achternaam { get; set; }
        public string Gender { get; set; }
        public string Voornaamkans { get; set; }
        public string Achternaamkans { get; set; }
        public string Gemeente { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public DateTime Geboortedatum { get; set; }
        public int BrondataId { get; set; } // gelinked aan de ID van versie
    }
}
