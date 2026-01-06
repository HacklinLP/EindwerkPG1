using KlantenSim_BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_BL.Model
{
    public class SimulatieInstellingen
    {
        public SimulatieInstellingen()
        {
            
        }
        public SimulatieInstellingen(string land, Dictionary<Gemeente, double> gemeentes, int aantalKlanten, int minLeeftijd, int maxLeeftijd, string opdrachtgever, int maxHuisnummer, double percentageMetLetter)
        {
            Land = land;
            Gemeentes = gemeentes;
            AantalKlanten = aantalKlanten;
            MinLeeftijd = minLeeftijd;
            MaxLeeftijd = maxLeeftijd;
            Opdrachtgever = opdrachtgever;
            MaxHuisnummer = maxHuisnummer;
            PercentageMetLetter = percentageMetLetter;
        }

        public int Id { get; set; }

        private string _land;

        public string Land
        {
            get => _land;
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) _land = value;
                else throw new InstellingenException("Gelieve een land te selecteren.");
            }
        }
        private Dictionary<Gemeente, double> _gemeentes;
        public Dictionary<Gemeente, double> Gemeentes
        {
            get => _gemeentes;
            set
            {
                double totaalPercentage = value.Values.Sum();
                if (totaalPercentage == 0)
                {
                    double gelijkeVerdeling = 100.0 / value.Count;

                    // Create a new dictionary to avoid modifying the collection while iterating
                    var updatedGemeentes = new Dictionary<Gemeente, double>();
                    foreach (var g in value.Keys)
                    {
                        updatedGemeentes[g] = gelijkeVerdeling;
                    }
                    _gemeentes = updatedGemeentes;
                }

                // Check of de som van de percentages wel 100 is
                // 0.0001 gebruikt omdat er soms afronding errors kunnen zijn met doubles
                else if (Math.Abs(totaalPercentage - 100.0) > 0.0001)
                {
                    throw new InstellingenException($"Het totaal van de percentages moet exact 100% zijn. Huidig totaal: {totaalPercentage}%");
                }
                else
                {
                    _gemeentes = value;
                }
                    
            }
        }

        private int _aantalKlanten;
        public int AantalKlanten
        {
            get => _aantalKlanten;
            set
            {
                if (value > 0) _aantalKlanten = value;
                else throw new InstellingenException("Het aantal klanten moet groter dan 0 zijn.");
            }
        }

        private int _minLeeftijd;

        public int MinLeeftijd
        {
            get => _minLeeftijd;
            set
            {
                if (value > 0) _minLeeftijd = value;
                else throw new InstellingenException("De minimumleeftijd moet groter dan 0 zijn.");
            }
        }

        private int _maxLeeftijd;

        public int MaxLeeftijd 
        {
            get => _maxLeeftijd;
            set
            {
                if (value >= MinLeeftijd) _maxLeeftijd = value;
                else throw new InstellingenException("De maximumleeftijd meot groter dan de minimumleeftijd zijn.");
            }
        }

        private string _opdrachtgever;
        public string Opdrachtgever
        {
            get => _opdrachtgever;
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) _opdrachtgever = value;
                else throw new InstellingenException("De opdrachtgever moet ingevuld zijn (niet leeg of witruimte).");
            }
        }
        private int _maxHuisnummer;

        public int MaxHuisnummer
        {
            get => _maxHuisnummer;
            set
            {
                if (value > 0) _maxHuisnummer = value;
                else throw new InstellingenException("Het maximale huisnummer moet groter zijn dan 0.");
            }
        }

        private double _percentageMetLetter;
        public double PercentageMetLetter
        {
            get => _percentageMetLetter;
            set 
            {
                if (value >= 0) _percentageMetLetter = value;
                else throw new InstellingenException("Het percentage moet groter dan 0 zijn.");
            }
        }
    }
}
