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
    public class NaamRepository : INaamRepository
    {
        private string _connectionString;

        public NaamRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void VoegVoornaamToe(string land, string versie, List<Voornaam> voornamen)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // NORMAAL GEZIEN zou het land al in onze DB moeten zitten aangezien ik de adressen eerst schrijf
                        // Er is dus geen nood aan checken of het land bestaat, ik vraag het gewoon op
                        // Dit moet wel aangepast worden stel je voegt geen adressen toe aan een databank en enkel namen (bij uitbreiding)

                        //STAP 1: Land (maak aan of vraag op)
                        string landSql = "IF NOT EXISTS (SELECT 1 FROM Land WHERE naam = @n) " +
                                         "INSERT INTO Land (naam) OUTPUT INSERTED.id VALUES (@n) " +
                                         "ELSE SELECT id FROM Land WHERE naam = @n";
                        SqlCommand landCmd = new SqlCommand(landSql, conn, transaction);
                        landCmd.Parameters.AddWithValue("@n", land);
                        int landId = (int)landCmd.ExecuteScalar();

                        //STAP 2: Versie (maak aan of vraag het op)
                        string versieSql = "IF NOT EXISTS (SELECT 1 FROM Versie WHERE landenid = @lid AND versie = @v) " +
                                           "INSERT INTO Versie (landenid, versie) OUTPUT INSERTED.id VALUES (@lid, @v) " +
                                           "ELSE SELECT id FROM Versie WHERE landenid = @lid AND versie = @v";
                        SqlCommand versieCmd = new SqlCommand(versieSql, conn, transaction);
                        versieCmd.Parameters.AddWithValue("@lid", landId);
                        versieCmd.Parameters.AddWithValue("@v", versie);
                        int dbVersieId = (int)versieCmd.ExecuteScalar();

                        // BULK DATA VOORBEREIDEN
                        DataTable voornaamTable = new DataTable();
                        voornaamTable.Columns.Add("frequency", typeof(string));
                        voornaamTable.Columns.Add("gender", typeof(string));
                        voornaamTable.Columns.Add("naam", typeof(string));
                        voornaamTable.Columns.Add("versieid", typeof(int));

                        //STAP 3: Voornamen toevoegen aan de datatable
                        foreach(var vn in voornamen)
                        {
                            voornaamTable.Rows.Add(vn.Frequency, vn.Gender, vn.Naam, vn.VersieId);
                        }

                        //STAP 4: Execute BulkCopy
                        using(SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                        {
                            bulkCopy.DestinationTableName = "Voornaam";

                            bulkCopy.ColumnMappings.Add("frequency", "frequency");
                            bulkCopy.ColumnMappings.Add("gender", "gender");
                            bulkCopy.ColumnMappings.Add("naam", "naam");
                            bulkCopy.ColumnMappings.Add("versieid", "versieid");

                            bulkCopy.WriteToServer(voornaamTable);
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

        public void VoegAchternaamToe(string landNaam, string versie, List<Achternaam> achternamen)
        {
            throw new NotImplementedException();
        }

    }
}
