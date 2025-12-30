using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Interfaces
{
    public interface INaamRepository
    {
        public void VoegVoornaamToe(string landNaam, string versie, List<Voornaam> voornamen);

        public void VoegAchternaamToe(string landNaam, string versie, List<Achternaam> achternamen);
    }
}
