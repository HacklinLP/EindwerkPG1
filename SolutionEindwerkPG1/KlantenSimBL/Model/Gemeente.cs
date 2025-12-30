using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class Gemeente
    {
        public Gemeente(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public Gemeente(int id, string naam, List<Straat> straten)
        {
            Id = id;
            Naam = naam;
            Straten = straten;
        }
        public Gemeente()
        {
            
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Straat> Straten { get; set; }
        public int VersieId { get; set; }

        public override string ToString()
        {
            return Naam;
        }

    }
}
