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
        public MySqlData SqlData = new MySqlData();
        public void PersonIdentification(string SecretCode)
        {
            string query = "SELECT * FROM people";
            bool IsNotExists = true;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                SqlData.OpenConnection();
                cmd = new MySqlCommand(query, SqlData.connection);
                reader = cmd.ExecuteReader();

                while (reader.Read() && IsNotExists)
                {
                    IsNotExists = reader.GetString("secret_code") == SecretCode ? false : true; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                SqlData.CloseConnection();
            }

            if (IsNotExists)
            {
                AddPerson(CreatePerson());
            }
        }

        public void AddPerson(Person person)
        {
            
            try
            {
                SqlData.OpenConnection();
                string Query = $"INSERT INTO people (first_name, last_name, secret_code, type, num_reports, num_mentions) VALUES ('{person.FirstName}', '{person.LastName}' , '{person.SecretCode}', '{person.Type}', '{person.NumReports}', '{person.NumMention}');";
                MySqlCommand cmd = new MySqlCommand(Query, SqlData.connection);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            finally
            {
                SqlData.CloseConnection();
            }
        }

        public Person CreatePerson()
        {
            Console.WriteLine("enter first name");
            string FirstName = Console.ReadLine()!;
            Console.WriteLine("enter last name");
            string LastName = Console.ReadLine()!;
            Console.WriteLine("enter secret code");
            string SecretCode = Console.ReadLine()!;
            Console.WriteLine("enter type, (reporter, target, both, potential_agent)");
            string Type = Console.ReadLine()!;
            Console.WriteLine("enter num report");
            string NumReports = Console.ReadLine()!;
            Console.WriteLine("enter num mention");
            string NumMention = Console.ReadLine()!;
            Console.WriteLine("enter first status (dangerous)");
            string Status = Console.ReadLine()!;
            Person person = new Person(FirstName,LastName,SecretCode,Type,NumReports ,NumMention); 
            return person;
        }
    }
}
