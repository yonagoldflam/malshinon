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
                    return new Person(FirstName, LastName, SecretCode, Type, NumReports, NumMentions, Id);
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
            Console.WriteLine(Id);
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
                    return new Person(FirstName, LastName, SecretCode, Type, NumReports, NumMentions, id);
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

        //public int GetIdBySecretCode(string SecretCode)
        //{

        //}
    }
}
