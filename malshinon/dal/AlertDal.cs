using malshinon.moddels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon.dal
{
    public class AlertDal
    {
        public void AddAlert(Alert alert)
        {
            try
            {
                Initialization.SqlData.OpenConnection();
                string Query = $"INSERT INTO alerts (target_id, reason) VALUES ({alert.TargetId}, '{alert.Reason}');";
                MySqlCommand cmd = new MySqlCommand(Query, Initialization.SqlData.connection);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
            }
        }

        public int DisplyAllAlerts()
        {
            string query = $"SELECT * FROM alerts";
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                Initialization.SqlData.OpenConnection();
                cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"time : {reader.GetDateTime("created_at")}, " +
                        $"reason: {reader.GetString("reason")}, ");

                    return reader.GetInt32("target_id");
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
            return -1;
        }
    }
}
