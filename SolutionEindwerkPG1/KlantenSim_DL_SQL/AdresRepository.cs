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
    }
}
