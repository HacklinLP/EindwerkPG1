using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Managers
{
    public class BestandManager
    {
        private readonly IAdresLezer _adresLezer;
        private readonly INaamLezer _namenLezer;

        public BestandManager(IAdresLezer adresLezer, INaamLezer namenLezer)
        {
            _adresLezer = adresLezer;
            _namenLezer = namenLezer;
        }

        // dit mag weg na het testen
        public void StartTestAdres(string pad)
        {
            //adres
            List<Gemeente> alleAdressen = _adresLezer.LeesAdressen(pad);

            Console.WriteLine($"{alleAdressen.Count()} ADRESSEN GELADEN");

            int adresCounter = 0;
            foreach (Gemeente g in alleAdressen)
            {
                Console.WriteLine($"Found: {g.Id}, {g.Naam}, {g.Straten.Count()}");
                adresCounter++;

                if (adresCounter == 5) break;
            }
        }

        public void StartTestNaam(string pad)
        {
            //naam
            List<Naam> alleNamen = _namenLezer.LeesNamen(pad);

            Console.WriteLine($"\n{alleNamen.Count()} NAMEN GELADEN");

            int namenCounter = 0;
            foreach (Naam n in alleNamen)
            {
                Console.WriteLine($"Found: {n.ID}, {n.NaamValue}, {n.Frequency}");
                namenCounter++;

                if (namenCounter == 5) break;
            }
        }
    }
}
