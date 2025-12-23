using KlantenSim_BL.Interfaces;
using KlantenSim_DL;
using KlantenSim_DL;

namespace KlantenSim_Utils
{
    public static class BestandLezerFactory
    {
        public static IAdresLezer MaakLezer()
        {
            return new CsvAdresLezer();
        }
    }
}
