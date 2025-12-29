using KlantenSim_BL.Interfaces;
using KlantenSim_DL;
using KlantenSim_DL.Readers;
using KlantenSim_DL_SQL;

namespace KlantenSim_Utils
{
    public static class BestandLezerFactory
    {
        public static IAdresLezer MaakTxtAdresLezer()
        {
            return new TxtAdresLezer();
        }

        public static INaamLezer MaakTxtNamenLezer()
        {
            return new TxtNamenLezer();
        }

        public static IAdresRepository MaakAdresRepository(string connectionString)
        {
            return new AdresRepository(connectionString);
        }
    }
}
