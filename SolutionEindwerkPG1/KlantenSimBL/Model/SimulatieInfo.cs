using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class SimulatieInfo
    {
        public int Id { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public int AantalKlantenAangemaakt { get; set; }
        public int JongsteLeeftijd { get; set; }
        public int OudsteLeeftijd { get; set; }
        public double GemiddeldeLeeftijd { get; set; }
        public int versieId { get; set; }
    }

    
}
