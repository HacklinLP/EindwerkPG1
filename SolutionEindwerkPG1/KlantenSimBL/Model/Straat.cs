namespace KlantenSim_BL.Model
{
    public class Straat
    {
        public Straat(int id, string naam, int gemeenteId)
        {
            Id = id;
            Naam = naam;
            GemeenteId = gemeenteId;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int VersieId { get; set; }
        public int GemeenteId { get; set; }
    }
}