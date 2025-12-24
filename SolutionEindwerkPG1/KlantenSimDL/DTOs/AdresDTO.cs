using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_DL.DTOs
{
    public class AdresDTO
    {
        public AdresDTO(string municipality, string street, string highwayType)
        {
            Municipality = municipality;
            Street = street;
            HighwayType = highwayType;
        }

        public string Municipality { get; set; }

        public string Street { get; set; }

        public string HighwayType { get; set; }


    }
}
