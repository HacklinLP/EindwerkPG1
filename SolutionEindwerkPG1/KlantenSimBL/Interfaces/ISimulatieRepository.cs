using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Interfaces
{
    public interface ISimulatieRepository
    {
        void BewaarSimulatie(SimulatieInfo info, SimulatieInstellingen instellingen, List<SimulatieKlant> klanten);
        List<SimulatieInfo> HaalSimulatieInfoOp();

        List<SimulatieKlant> HaalSimulatieKlantenOp(int simId);
    }
}
