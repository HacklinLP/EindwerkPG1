using KlantenSim_BL.Interfaces;
using KlantenSim_BL.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KlantenSim_DL_SQL
{
    public class AdresRepository : IAdresRepository
    {
        private string _connectionString;

        public AdresRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        #region DataReader
        public void VoegAdresToe(string land, string versie, List<Gemeente> gemeentes)
        {
            // TODO: Vraag zeker nog eens uitleg over dit aan Gemini

            // string land == Belgie
            // string versie == 1
            // loop gemeente per gemeente door de lijst

            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // STAP 1: Land (Maak het aan of vraag het op als het al bestaat)
                        // Begrijp ik
                        string landSql = "IF NOT EXISTS (SELECT 1 FROM Land WHERE naam = @n) " +
                                         "INSERT INTO Land (naam) OUTPUT INSERTED.id VALUES (@n) " +
                                         "ELSE SELECT id FROM Land WHERE naam = @n";
                        SqlCommand landCmd = new SqlCommand(landSql, conn, transaction);
                        landCmd.Parameters.AddWithValue("@n", land);
                        int landId = (int)landCmd.ExecuteScalar();

                        // STAP 2: Versie (Maak het aan of vraag het op als het al bestaat)
                        // Begrijp ik
                        string versieSql = "IF NOT EXISTS (SELECT 1 FROM Versie WHERE landenid = @lid AND versie = @v) " +
                                           "INSERT INTO Versie (landenid, versie) OUTPUT INSERTED.id VALUES (@lid, @v) " +
                                           "ELSE SELECT id FROM Versie WHERE landenid = @lid AND versie = @v";
                        SqlCommand versieCmd = new SqlCommand(versieSql, conn, transaction);
                        versieCmd.Parameters.AddWithValue("@lid", landId);
                        versieCmd.Parameters.AddWithValue("@v", versie);
                        int dbVersieId = (int)versieCmd.ExecuteScalar();


                        // BULK DATA VOORBEREIDEN: Create DataTable for all streets --  je kan dit lett. zien als een virtuele tabel
                        DataTable straatTable = new DataTable();
                        straatTable.Columns.Add("naam", typeof(string));
                        straatTable.Columns.Add("gemeenteid", typeof(int));
                        straatTable.Columns.Add("versieid", typeof(int));

                        // STAP 3: Gemeentes doorlopen
                        foreach (var g in gemeentes)
                        {
                            string gemeenteSql = "INSERT INTO Gemeente (naam, versieid) OUTPUT INSERTED.id VALUES (@naam, @vid)";
                            SqlCommand gemeenteCmd = new SqlCommand(gemeenteSql, conn, transaction);
                            gemeenteCmd.Parameters.AddWithValue("@naam", g.Naam);
                            gemeenteCmd.Parameters.AddWithValue("@vid", dbVersieId);
                            int huidigeGemeenteId = (int)gemeenteCmd.ExecuteScalar(); // Capture Identity (?)

                            // STAP 4: Voeg straten toe aan de DataTable ipv NU te inserten
                            foreach (var s in g.Straten)
                            {
                                straatTable.Rows.Add(s.Naam, huidigeGemeenteId, dbVersieId);
                            }
                            // DIT IS DE OUDE MANIER VAN STRATEN INSERTEN -- NU GEBRUIK IK SQLBULKCOPY OMDAT DIT BETER IS
                            //foreach (var s in g.Straten)
                            //{
                            //    string stratenSql = "INSERT INTO Straat (naam, gemeenteid, versieid) VALUES (@naam, @gid, @vid)";
                            //    SqlCommand stratenCmd = new SqlCommand(stratenSql, conn, transaction);
                            //    stratenCmd.Parameters.AddWithValue("@naam", s.Naam);
                            //    stratenCmd.Parameters.AddWithValue("@gid", huidigeGemeenteId); // Link to Parent
                            //    stratenCmd.Parameters.AddWithValue("@vid", dbVersieId);
                            //    stratenCmd.ExecuteNonQuery();
                            //}
                        }

                        // STAP 5: Execute SqlBulkCopy voor ALLE straten in 1 keer!
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                        {
                            bulkCopy.DestinationTableName = "Straat";

                            // Expliciet mappen van DataTable columns -> DB columns
                            bulkCopy.ColumnMappings.Add("naam", "naam");
                            bulkCopy.ColumnMappings.Add("gemeenteid", "gemeenteid");
                            bulkCopy.ColumnMappings.Add("versieid", "versieid");

                            bulkCopy.WriteToServer(straatTable);
                        }
                        
                        transaction.Commit(); // SUCCESS
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // FAIL
                        throw;
                    }
                }
            }
        }
        #endregion

        #region UI -> Info weergeven vooraf
        public List<Gemeente> GeefGemeentesVoorLand(string landNaam)
        {
            List<Gemeente> gemeentes = new List<Gemeente>();

            string sql = @"SELECT DISTINCT g.id, g.naam 
                   FROM Gemeente g 
                   JOIN Versie v ON g.versieid = v.id 
                   JOIN Land l ON v.landenid = l.id 
                   WHERE l.naam = @landNaam 
                   ORDER BY g.naam";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@landNaam", landNaam);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gemeentes.Add(new Gemeente
                        {
                            Id = (int)reader["id"],
                            Naam = reader["naam"].ToString()
                        });
                    }
                }
            }
            return gemeentes;
        }
        public List<string> GeefAlleLanden()
        {
            List<string> landen = new List<string>();
            string sql = "SELECT naam FROM Land ORDER BY naam";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        landen.Add(reader["naam"].ToString());
                    }
                }
            }
            return landen;
        }
        #endregion

        #region SimManager
        public int GeefVersieIdVoorLand(string land)
        {
            int versieId;

            string sql = @"SELECT v.id 
                         FROM Versie v
                         JOIN Land l ON v.landenid = l.id
                         WHERE l.naam = @landNaam";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@landNaam", land);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        versieId = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception($"Geen versie gevonden voor land: {land}");
                    }
                }
                catch (Exception ex)
                {
                    // Log de fout of gooi hem door naar de Manager
                    throw new Exception("Fout bij het ophalen van VersieID", ex);
                }
            }
            return versieId;
        }

        public List<Straat> GeefStratenVoorGemeentes(List<int> gemeenteIds, int versieId)
        {
            List<Straat> straten = new List<Straat>();

            // We bouwen de "IN" clause voor SQL: bijv. (1, 5, 8)
            // Dit doen we door de lijst van ints om te zetten naar een string gescheiden door komma's.
            string idLijst = string.Join(",", gemeenteIds);

            // De query filtert op de lijst van gemeentes EN de specifieke versieid
            string sql = $"SELECT id, naam, gemeenteid, versieid FROM Straat " +
                         $"WHERE gemeenteid IN ({idLijst}) AND versieid = @vId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@vId", versieId);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            straten.Add(new Straat
                            {
                                Id = (int)reader["id"],
                                Naam = reader["naam"].ToString(),
                                GemeenteId = (int)reader["gemeenteid"],
                                VersieId = (int)reader["versieid"]
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Fout bij het ophalen van straten voor de geselecteerde gemeentes. (Hoogstwaarschijnlijk geen gemeentes aangeduidt)", ex);
                }
            }
            return straten;
        }
        #endregion
    }
}
