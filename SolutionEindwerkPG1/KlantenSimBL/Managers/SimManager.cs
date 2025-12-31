using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Managers
{
    public class SimManager
    {
        private readonly IAdresRepository _adresRepo;
        private readonly INaamRepository _naamRepo;
        private readonly Random _rng = new Random();

        public SimManager(IAdresRepository adresRepo, INaamRepository naamRepo)
        {
            _adresRepo = adresRepo;
            _naamRepo = naamRepo;
        }

        public List<SimulatieKlant> StartSimulatie(SimulatieInstellingen instellingen)
        {
            // --- 1. Data ophalen uit de repo's ---
            int versieId = _adresRepo.GeefVersieIdVoorLand(instellingen.Land);

            // adres
            List<int> gemeenteIds = instellingen.Gemeentes.Keys.Select(g => g.Id).ToList();
            List<Straat> beschikbareStraten = _adresRepo.GeefStratenVoorGemeentes(gemeenteIds, versieId);

            // naam
            List<Voornaam> voornamen = _naamRepo.GeefVoornamenVoorVersie(versieId);
            List<Achternaam> achternamen = _naamRepo.GeefAchternamenVoorVersie(versieId);

            // lijst die de resultaten zal bevatten van de gesimuleerde klanten
            List<SimulatieKlant> klantenLijst = new List<SimulatieKlant>();

            // Bereken de totalen één keer vooraf
            double totaalFrequentieVoornamen = voornamen.Sum(v => v.Frequency ?? 0);
            double totaalFrequentieAchternamen = achternamen.Sum(a => a.Frequency ?? 0);

            // --- 2. De loop om klanten te genereren ---
            for (int i = 0; i < instellingen.AantalKlanten; i++)
            {
                int id = i + 1;
                // A. Kies Gemeente (Gewogen loting op basis van Dictionary uit UI)
                var gekozenGemeente = KiesGewogenItem(instellingen.Gemeentes.Keys, g => instellingen.Gemeentes[g]);

                // B. Kies Straat (Willekeurige straat binnen de gekozen gemeente)
                var stratenInGemeente = beschikbareStraten.Where(s => s.GemeenteId == gekozenGemeente.Id).ToList();
                var gekozenStraat = stratenInGemeente[_rng.Next(stratenInGemeente.Count)];

                // C. Kies Namen (Gewogen loting op basis van Frequency uit DB)
                var voornaamObj = KiesGewogenItem(voornamen, v => v.Frequency ?? 0);
                var achternaamObj = KiesGewogenItem(achternamen, a => a.Frequency ?? 0);

                // D. Huisnummer genereren (met eventuele letter)
                string huisnummer = GenereerHuisnummer(instellingen.MaxHuisnummer, instellingen.PercentageMetLetter);

                // E. Leeftijd / Geboortedatum (tussen min en max)
                int leeftijd = _rng.Next(instellingen.MinLeeftijd, instellingen.MaxLeeftijd + 1);
                DateTime geboortedatum = DateTime.Now.AddYears(-leeftijd).AddDays(-_rng.Next(0, 365));

                // Bepaal per aanmaak de percentages van voorkomen
                double voornaampercentage = (voornaamObj.Frequency.GetValueOrDefault(0) / totaalFrequentieVoornamen) * 100;
                double achternaampercentage = (achternaamObj.Frequency.GetValueOrDefault(0) / totaalFrequentieAchternamen) * 100;

                // F. Voeg toe aan de lijst
                klantenLijst.Add(new SimulatieKlant
                {
                    Id = id,
                    Voornaam = voornaamObj.Naam,
                    Achternaam = achternaamObj.Naam,
                    Gender = voornaamObj.Gender,
                    Voornaamkans = $"{voornaampercentage:F4}%",
                    Achternaamkans = $"{achternaampercentage:F4}%",
                    Straat = gekozenStraat.Naam,
                    Huisnummer = huisnummer,
                    Gemeente = gekozenGemeente.Naam,
                    Geboortedatum = geboortedatum,
                    BrondataId = versieId
                    // Opdrachtgever = instellingen.Opdrachtgever ----- DENK NIET DAT DIT BIJ DE SIMULATIEKLANT MOET
                });
            }
            return klantenLijst;
        }

        // --- Hulp methodes voor de logica ---
        #region Hulpmethodes
        private T KiesGewogenItem<T>(IEnumerable<T> items, Func<T, double> gewichtSelector)
        {
            double totaalGewicht = items.Sum(gewichtSelector);
            double r = _rng.NextDouble() * totaalGewicht;
            double huidigeSom = 0;

            foreach (var item in items)
            {
                huidigeSom += gewichtSelector(item);
                if (r <= huidigeSom) return item;
            }
            return items.Last();
        }

        private string GenereerHuisnummer(int max, double kansOpLetter)
        {
            int nr = _rng.Next(1, max + 1);
            string result = nr.ToString();

            // Rol een getal tussen 0 en 100 om te zien of we een letter toevoegen
            if (_rng.NextDouble() * 100 <= kansOpLetter)
            {
                char letter = (char)('A' + _rng.Next(0, 26));
                result += letter;
            }
            return result;
        }
        #endregion
    }
}
