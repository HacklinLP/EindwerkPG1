using KlantenSim_BL.Interfaces;
using KlantenSim_DL;
using KlantenSim_DL;

namespace KlantenSim_Utils
{
    public static class BestandLezerFactory
    {
        public static IAdresLezer MaakTxtAdresLezer()
        {
            return new TxtAdresLezer();
        }

        public static INamenLezer MaakTxtNamenLezer()
        {
            return new TxtNamenLezer();
        }
    }
}
