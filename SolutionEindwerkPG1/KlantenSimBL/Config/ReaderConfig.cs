using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Config
{
    public class LandConfig
    {
        public Dictionary<string, VersieInstellingen> Versie { get; set; }
    }

    public class VersieInstellingen
    {
        public VoornaamInstellingen VoornaamInstellingen { get; set; }
        public AchternaamInstellingen AchternaamInstellingen { get; set; }
        public AdresInstellingen AdresInstellingen { get; set; }
    }
    public class AdresInstellingen
    {
        public string Pad { get; set; }
        public int SkipLines { get; set; }
        public int MunIndex { get; set; }
        public int StraatIndex { get; set; }      
        public int HighwayTypeIndex { get; set; }
        public string Separator { get; set; }
        public List<string>? VerbodenMunWoorden { get; set; }
        public List<string> ToegestaneMun { get; set; }
    }
    public class VoornaamInstellingen
    {
        public List<BestandenConfig> Bestanden { get; set; }
        public int SkipLines { get; set; }
        public int NaamIndex { get; set; }
        public int FrequencyIndex { get; set; }
        public string Separator { get; set; }
    }
    public class AchternaamInstellingen
    {
        public List<BestandenConfig> Bestanden { get; set; }
        public int SkipLines { get; set; }
        public int NaamIndex { get; set; }
        public int FrequencyIndex { get; set; }
        public string Separator { get; set; }
    }
    public class BestandenConfig
    {
        public string Pad { get; set; }
        public string Gender { get; set; }
        public int? FrequencyIndex { get; set; }
    }

    
}
