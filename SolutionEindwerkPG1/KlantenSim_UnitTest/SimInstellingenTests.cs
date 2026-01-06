using KlantenSim_BL.Exceptions;
using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;

namespace KlantenSim_UnitTest
{
    public class SimulatieInstellingenTests
    {
        [Fact]
        public void Land_Geldig()
        {
            // Arrange
            var instellingen = new SimulatieInstellingen();
            string geldigLand = "België";

            // Act
            instellingen.Land = geldigLand;

            // Assert
            Assert.Equal(geldigLand, instellingen.Land);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Land_Ongeldig_Exception(string ongeldigLand)
        {
            var instellingen = new SimulatieInstellingen();
            Assert.Throws<InstellingenException>(() => instellingen.Land = ongeldigLand);
        }

        [Fact]
        public void Gemeentes_GeldigePercenten()
        {
            var instellingen = new SimulatieInstellingen();
            var data = new Dictionary<Gemeente, double>
            {
                { new Gemeente { Id = 1, Naam = "Gent" }, 60.0 },
                { new Gemeente { Id = 2, Naam = "Antwerpen" }, 40.0 }
            };

            instellingen.Gemeentes = data;

            Assert.Equal(2, instellingen.Gemeentes.Count);
            Assert.Equal(100.0, instellingen.Gemeentes.Values.Sum());
        }

        [Fact]
        public void Gemeentes_AllenNul_MoetEvenVerdelen()
        {
            var instellingen = new SimulatieInstellingen();
            var data = new Dictionary<Gemeente, double>
            {
                { new Gemeente { Id = 1 }, 0.0 },
                { new Gemeente { Id = 2 }, 0.0 },
                { new Gemeente { Id = 3 }, 0.0 },
                { new Gemeente { Id = 4 }, 0.0 }
            };

            instellingen.Gemeentes = data;

            // 100 / 4 = 25 per gemeente
            Assert.All(instellingen.Gemeentes.Values, v => Assert.Equal(25.0, v));
        }

        [Fact]
        public void Gemeentes_OngeldigeSom_Exception()
        {
            var instellingen = new SimulatieInstellingen();
            var data = new Dictionary<Gemeente, double> { { new Gemeente(), 50.0 } };

            var ex = Assert.Throws<InstellingenException>(() => instellingen.Gemeentes = data);
            Assert.Contains("exact 100% zijn", ex.Message);
        }

        [Fact]
        public void AantalKlanten_Geldig()
        {
            var instellingen = new SimulatieInstellingen();
            instellingen.AantalKlanten = 500;
            Assert.Equal(500, instellingen.AantalKlanten);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AantalKlanten_Ongeldig_Exception(int ongeldigAantal)
        {
            var instellingen = new SimulatieInstellingen();
            Assert.Throws<InstellingenException>(() => instellingen.AantalKlanten = ongeldigAantal);
        }

        [Fact]
        public void Leeftijd_MaxLagerDanMin_Exception()
        {
            var instellingen = new SimulatieInstellingen();
            instellingen.MinLeeftijd = 18;

            // Act & Assert
            var ex = Assert.Throws<InstellingenException>(() => instellingen.MaxLeeftijd = 17);
            Assert.Contains("groter dan de minimumleeftijd", ex.Message);
        }

        [Fact]
        public void Leeftijd_GeldigeRange()
        {
            var instellingen = new SimulatieInstellingen();
            instellingen.MinLeeftijd = 18;
            instellingen.MaxLeeftijd = 99;

            Assert.Equal(18, instellingen.MinLeeftijd);
            Assert.Equal(99, instellingen.MaxLeeftijd);
        }

        [Fact]
        public void Opdrachtgever_Geldig()
        {
            var instellingen = new SimulatieInstellingen();
            instellingen.Opdrachtgever = "Stad Gent";
            Assert.Equal("Stad Gent", instellingen.Opdrachtgever);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Opdrachtgever_Ongeldig_Exception(string input)
        {
            var instellingen = new SimulatieInstellingen();
            Assert.Throws<InstellingenException>(() => instellingen.Opdrachtgever = input);
        }

        [Fact]
        public void MaxHuisnummer_Valid()
        {
            var instellingen = new SimulatieInstellingen();
            instellingen.MaxHuisnummer = 250;
            Assert.Equal(250, instellingen.MaxHuisnummer);
        }

        [Fact]
        public void PercentageMetLetter_OngeldigNegatief_Exception()
        {
            var instellingen = new SimulatieInstellingen();
            Assert.Throws<InstellingenException>(() => instellingen.PercentageMetLetter = -5.0);
        }

        [Fact]
        public void Constructor_MetParameters_ZouCorrectMoetenZijn()
        {
            // Test of constructor met parameters de validatie ook correct doorloopt
            var gemeentes = new Dictionary<Gemeente, double> { { new Gemeente { Id = 1 }, 100.0 } };

            var instellingen = new SimulatieInstellingen(
                "Nederland", gemeentes, 100, 18, 50, "TestCorp", 100, 15.5);

            Assert.Equal("Nederland", instellingen.Land);
            Assert.Equal(100, instellingen.AantalKlanten);
            Assert.Equal(15.5, instellingen.PercentageMetLetter);
        }
    }
}
