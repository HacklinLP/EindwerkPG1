using KlantenSim_BL.Config;
using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Interfaces
{
    public interface INaamLezer
    {
        List<Voornaam> LeesVoornamen(BestandenConfig bestand, VoornaamInstellingen instellingen, int versieId);
        List<Achternaam> LeesAchternamen(BestandenConfig bestand, AchternaamInstellingen instellingen, int versieId);
    }
}
