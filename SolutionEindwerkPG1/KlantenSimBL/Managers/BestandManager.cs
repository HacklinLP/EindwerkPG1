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

        public BestandManager(IAdresLezer adresLezer)
        {
            _adresLezer = adresLezer;
        }

        // dit mag weg na het testen
        public void StartTest(string pad)
        {
            List<Adres> alleAdressen = _adresLezer.LeesAdressen(pad);

            Console.WriteLine($"{alleAdressen.Count()} adressen geladen");

            int counter = 0;
            foreach (Adres a in alleAdressen)
            {
                Console.WriteLine($"Found: {a.Gemeente}, {a.Straat}, {a.HighwayType}");
                counter++;

                if (counter == 50) break;
            }
        }
    }
}
