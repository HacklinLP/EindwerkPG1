using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class Adres
    {
        public Adres(string gemeente, string straat, string huisnummer)
        {
            Gemeente = gemeente;
            Straat = straat;
            Huisnummer = huisnummer;
        }

        public string Gemeente { get; set; }

        public string Straat { get; set; }

        public string Huisnummer { get; set; }
    }
}
