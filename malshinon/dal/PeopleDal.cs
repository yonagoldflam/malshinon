using Google.Protobuf.Compiler;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon.db;
using malshinon.moddels;
using System.Reflection.PortableExecutable;
using System.Reflection.Metadata.Ecma335;

namespace malshinon.dal
{
    public class PeopleDal
    {

        public void AddPerson(Person person)
        {           
            try
            {
                Initialization.SqlData.OpenConnection();
                string Query = $"INSERT INTO people (first_name, last_name, secret_code, type) VALUES ('{person.FirstName}', '{person.LastName}' , '{person.SecretCode}', '{person.Type}');";
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

        public Person CreatePerson(string SecretCode=null, string Type=null)
        {
            Console.WriteLine("enter first name");
            string FirstName = Console.ReadLine()!;
            Console.WriteLine("enter last name");
            string LastName = Console.ReadLine()!;
            if (SecretCode == null)
            {
                Console.WriteLine("enter secret code");
                SecretCode = Console.ReadLine()!;
            }
            if (Type == null)
            {
                Console.WriteLine("enter type, (reporter, target, both, potential_agent)");
                Type = Console.ReadLine()!;
            }
            Console.WriteLine("enter status (dangerous)");
            string Status = Console.ReadLine()!;
            Person person = new Person(FirstName,LastName,SecretCode,Type); 
            return person;
        }

        public Person GetPersonBySecretCode(string secretCode)
        {
            string query = $"SELECT * FROM people WHERE secret_code = '{secretCode}'";
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                Initialization.SqlData.OpenConnection();
                cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int Id = reader.GetInt32("id");
                    string FirstName = reader.GetString("first_name");
                    string LastName = reader.GetString("last_name");
                    string SecretCode = reader.GetString("secret_code");
                    string Type = reader.GetString("type");
                    int NumReports = reader.GetInt32("num_reports");
                    int NumMentions = reader.GetInt32("num_mentions");
                    string Status = reader.GetString("status");
                    return new Person(FirstName, LastName, SecretCode, Type, NumReports, NumMentions, Status, Id);
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

            return null;
        }

        public Person GetPersonById(int Id)
        {
            string query = $"SELECT * FROM people WHERE id = {Id}";
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                Initialization.SqlData.OpenConnection();
                cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string FirstName = reader.GetString("first_name");
                    string LastName = reader.GetString("last_name");
                    string SecretCode = reader.GetString("secret_code");
                    string Type = reader.GetString("type");
                    int NumReports = reader.GetInt32("num_reports");
                    int NumMentions = reader.GetInt32("num_mentions");
                    string Status = reader.GetString("status");
                    return new Person(FirstName, LastName, SecretCode, Type, NumReports, NumMentions, Status, id);
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

            return null;
        }

        public void UpdateNumReports(int Id)
        {
            try
            {
                Initialization.SqlData.OpenConnection();
                string query = $"UPDATE people SET num_reports = num_reports + 1 WHERE id = {Id}";
                MySqlCommand cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
            }
        }

        public void UpdateNumMentions(int Id)
        {
            try
            {
                Initialization.SqlData.OpenConnection();
                string query = $"UPDATE people SET num_mentions = num_mentions + 1 WHERE id = {Id}";
                MySqlCommand cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
            }
        }


        
        public void UpdateType(int personId, string newType)
        {
            try
            {
                Initialization.SqlData.OpenConnection();
                string query = $"UPDATE people SET type = '{newType}' WHERE id = {personId}";
                MySqlCommand cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
            }
        }

        public bool IsDangerous(int TargetId)
        {
            string query = $"SELECT * FROM people WHERE id = {TargetId}";
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                Initialization.SqlData.OpenConnection();
                cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32("num_mentions") >= 20)
                        return true;
                    
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
            return false;
        }

        public void UpdateStatus(int PersonId)
        {
            try
            {
                Initialization.SqlData.OpenConnection();
                string query = $"UPDATE people SET status = '{"dangerous"}' WHERE id = {PersonId}";
                MySqlCommand cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
            }
        }

        public int PrintDangerousTargets()
        {
            string query = $"SELECT * FROM people WHERE status = '{"dangerous"}'";
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                Initialization.SqlData.OpenConnection();
                cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"first name: {reader.GetString("first_name")}, " +
                        $"last name: {reader.GetString("last_name")}, " +
                        $"sum reports: {reader.GetInt32("num_mentions")}, " );

                    return reader.GetInt32("id");
                        
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

        public int DisplyPotentialAgent()
        {
            string query = $"SELECT * FROM people WHERE type = '{"potential_agent"}'";
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                Initialization.SqlData.OpenConnection();
                cmd = new MySqlCommand(query, Initialization.SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"first name: {reader.GetString("first_name")}, " +
                        $"last name: {reader.GetString("last_name")}, " +
                        $"sum reports: {reader.GetInt32("num_reports")}, ");

                    return reader.GetInt32("id");
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
