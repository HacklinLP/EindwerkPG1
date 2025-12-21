using KlantenSim_BL.Interfaces;

namespace KlantenSimDL
{
    public class AdresLezer : IAdresLezer
    {
        public List<string[]> LeesAdressen(string pad)
        {
            var resultaat = new List<string[]>();

            if (!File.Exists(pad)) return resultaat;

            using (StreamReader sr = new StreamReader(pad))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var lijn = sr.ReadLine();
                    resultaat.Add(lijn.Split(","));
                }
            }
            return resultaat;
        }
    }
}
