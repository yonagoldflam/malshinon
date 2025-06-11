using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Google.Protobuf.Compiler;
using malshinon.db;
using malshinon.moddels;
using MySql.Data.MySqlClient;

namespace malshinon.dal
{
    public class IntelReportsDal
    {

        public bool Report(int ReporterId, int TargetId, string ReportText)
        {             
            try
            {
                Initialization.SqlData.OpenConnection();
                string Query = $"INSERT INTO intel_reports (reporter_id, target_id, text) VALUES ('{ReporterId}', '{TargetId}', '{ReportText}' );";
                MySqlCommand cmd = new MySqlCommand(Query, Initialization.SqlData.connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
            }

            return false;
        }

        public bool CheckHave10ReportsWith100AvgLetters(int ReporterId)
        {
            string query = $"SELECT * FROM intel_reports WHERE reporter_id = {ReporterId}";
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            int Counter = 0;
            int CountLetters = 0;

            try
            {
                Initialization.SqlData.OpenConnection();
                cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Counter++;
                    CountLetters += reader.GetString("text").Length;
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
            }
            return Counter >= 10 && (CountLetters / Counter) >= 100 ? true : false;
        }
    }
}
