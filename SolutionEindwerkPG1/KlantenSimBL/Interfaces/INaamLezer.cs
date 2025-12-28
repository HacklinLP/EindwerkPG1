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
        List<Voornaam> LeesVoorNamen(BestandenConfig bestand, VoornaamInstellingen instellingen, int versieId);
    }
}
