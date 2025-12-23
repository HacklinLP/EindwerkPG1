using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class Adres
    {
        public Adres(string gemeente, string straat, string highwaytype)
        {
            Gemeente = gemeente;
            Straat = straat;
            HighwayType = highwaytype;
        }

        public string Gemeente { get; set; }

        public string Straat { get; set; }

        public string HighwayType { get; set; }
    }
}
