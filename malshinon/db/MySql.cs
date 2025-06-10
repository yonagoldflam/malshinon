using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace malshinon.db
{
    public class MySqlData
    {
        public string connStr = "Server=localhost;Database=malshinondb;User=root;Password='';Port=3306;";
        public MySqlConnection connection;

        public void CheckConnect()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                Console.WriteLine("good");
                conn.Close();
            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"eror: {ex.Message}");
            }
        }

        public MySqlConnection OpenConnection()
        {

                connection = new MySqlConnection(connStr);

                connection.Open();
                Console.WriteLine("Connection successful.");
            

            return connection;

        }

        public void CloseConnection()
        {
            connection.Close();
            connection = null;
        }
    }
}
