using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Interfaces
{
    public interface IAdresRepository
    {
        // Ik stuur de versie als een string naar de repo (laat aanpassingen toe voor later en vermijd verwarring met de PK id van versie)
        void VoegAdresToe(string landNaam, string versie, List<Gemeente> gemeentes);

        List<string> GeefAlleLanden();
        List<Gemeente> GeefGemeentesVoorLand(string landNaam);
    }
}
