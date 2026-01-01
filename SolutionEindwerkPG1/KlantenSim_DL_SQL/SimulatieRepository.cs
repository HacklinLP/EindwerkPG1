using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSim_DL_SQL
{
    public class SimulatieRepository : ISimulatieRepository
    {
        private string _connectionString;

        public SimulatieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void BewaarSimulatie(SimulatieInfo info, SimulatieInstellingen instellingen, List<SimulatieKlant> klanten)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Bewaar SimulatieInfo en krijg ID ervan
                        string sqlInfo = @"INSERT INTO SimulatieInfo (aanmaakdatum, aantalaangemaakt, jongsteleeftijd, oudsteleeftijd, gemiddeldeleeftijd, versieid, opdrachtgever) 
                                   OUTPUT INSERTED.id 
                                   VALUES (@datum, @aantal, @min, @max, @gem, @vId, @opdrachtgever)";

                        SqlCommand cmdInfo = new SqlCommand(sqlInfo, conn, trans);
                        cmdInfo.Parameters.AddWithValue("@datum", info.AanmaakDatum);
                        cmdInfo.Parameters.AddWithValue("@aantal", info.AantalKlantenAangemaakt);
                        cmdInfo.Parameters.AddWithValue("@min", info.JongsteLeeftijd);
                        cmdInfo.Parameters.AddWithValue("@max", info.OudsteLeeftijd);
                        cmdInfo.Parameters.AddWithValue("@gem", info.GemiddeldeLeeftijd);
                        cmdInfo.Parameters.AddWithValue("@vId", info.versieId);
                        cmdInfo.Parameters.AddWithValue("@opdrachtgever", instellingen.Opdrachtgever);

                        int nieuweSimId = (int)cmdInfo.ExecuteScalar();

                        // 2. Bewaar SimulatieInstellingen
                        string sqlInst = @"INSERT INTO SimulatieInstellingen (land, aantalklanten, minleeftijd, maxleeftijd, opdrachtgever, maxhuisnummer, percentagemetletter, siminfoid) 
                                            OUTPUT INSERTED.id
                                                VALUES (@land, @aantal, @min, @max, @opdr, @maxH, @perc, @simId)";

                        SqlCommand cmdInst = new SqlCommand(sqlInst, conn, trans);
                        cmdInst.Parameters.AddWithValue("@land", instellingen.Land);
                        cmdInst.Parameters.AddWithValue("@aantal", instellingen.AantalKlanten);
                        cmdInst.Parameters.AddWithValue("@min", instellingen.MinLeeftijd);
                        cmdInst.Parameters.AddWithValue("@max", instellingen.MaxLeeftijd);
                        cmdInst.Parameters.AddWithValue("@opdr", instellingen.Opdrachtgever);
                        cmdInst.Parameters.AddWithValue("@maxH", instellingen.MaxHuisnummer);
                        cmdInst.Parameters.AddWithValue("@perc", instellingen.PercentageMetLetter);
                        cmdInst.Parameters.AddWithValue("@simId", nieuweSimId);

                        int InstellingId = (int)cmdInst.ExecuteScalar();

                        // 2,5. Bewaar GemeenteInstellingen
                        string sqlGemInst = @"INSERT INTO GemeenteInstellingen (naam, percentage, siminstellingid) 
                                            VALUES (@naam, @perc, @instId)";

                        foreach (var entry in instellingen.Gemeentes)
                        {
                            SqlCommand cmdGem = new SqlCommand(sqlGemInst, conn, trans);
                            cmdGem.Parameters.AddWithValue("@naam", entry.Key.Naam); // entry.Key is je Gemeente object
                            cmdGem.Parameters.AddWithValue("@perc", entry.Value);    // entry.Value is de double (percentage)
                            cmdGem.Parameters.AddWithValue("@instId", InstellingId);
                            cmdGem.ExecuteNonQuery();
                        }

                        // 3. Bewaar alle Klanten (SqlBulkCopy zou eigl veel sneller geweest zijn om te uploaden maar het is veel meer code)

                        string sqlKlant = @"INSERT INTO SimulatieKlant (voornaam, achternaam, gender, voornaamkans, achternaamkans, gemeente, straat, huisnummer, geboortedatum, siminfoid) 
                                    VALUES (@v, @a, @g, @vk, @ak, @gem, @str, @h, @geb, @simId)";

                        foreach (var k in klanten)
                        {
                            SqlCommand cmdKlant = new SqlCommand(sqlKlant, conn, trans);
                            cmdKlant.Parameters.AddWithValue("@v", k.Voornaam);
                            cmdKlant.Parameters.AddWithValue("@a", k.Achternaam);
                            cmdKlant.Parameters.AddWithValue("@g", k.Gender);
                            cmdKlant.Parameters.AddWithValue("@vk", k.Voornaamkans);
                            cmdKlant.Parameters.AddWithValue("@ak", k.Achternaamkans);
                            cmdKlant.Parameters.AddWithValue("@gem", k.Gemeente);
                            cmdKlant.Parameters.AddWithValue("@str", k.Straat);
                            cmdKlant.Parameters.AddWithValue("@h", k.Huisnummer);
                            cmdKlant.Parameters.AddWithValue("@geb", k.Geboortedatum);
                            cmdKlant.Parameters.AddWithValue("@simId", nieuweSimId);
                            cmdKlant.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
