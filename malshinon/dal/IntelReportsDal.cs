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

        public void Report()
        {
            bool Done = false;
            
            Console.WriteLine("enter your secret code");
            string RSC = Console.ReadLine();
            Console.WriteLine("enter target secret code");
            string TSC = Console.ReadLine();
            Console.WriteLine("enter report text");
            string Text = Console.ReadLine();


            Person ReporterPerson = Initialization.PersonDalIns.GetPersonBySecretCode(RSC);
            if (ReporterPerson == null)
            {
                Console.WriteLine("You are identified as a new user in the system, please enter your details: ");
                ReporterPerson = Initialization.PersonDalIns.CreatePerson(RSC, "reporter");
                Initialization.PersonDalIns.AddPerson(ReporterPerson);
                ReporterPerson = Initialization.PersonDalIns.GetPersonBySecretCode(RSC);            
            }

            Person TargetPerson = Initialization.PersonDalIns.GetPersonBySecretCode(TSC);
            if (TargetPerson == null)
            {
                Console.WriteLine("The system does not recognize the target's details. Please enter their details:");
                TargetPerson = Initialization.PersonDalIns.CreatePerson(TSC, "target");
                Initialization.PersonDalIns.AddPerson(TargetPerson);
                TargetPerson = Initialization.PersonDalIns.GetPersonBySecretCode(TSC);
            }

            try
            {
                Initialization.SqlData.OpenConnection();
                string Query = $"INSERT INTO intel_reports (reporter_id, target_id, text) VALUES ('{ReporterPerson.Id}', '{TargetPerson.Id}', '{Text}' );";
                MySqlCommand cmd = new MySqlCommand(Query, Initialization.SqlData.connection);
                cmd.ExecuteNonQuery();
                Done = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            finally
            {
                Initialization.SqlData.CloseConnection();
                if (Done)
                {                    
                    Initialization.PersonDalIns.UpdateNumReports(ReporterPerson.Id);
                    Initialization.PersonDalIns.UpdateNumMentions(TargetPerson.Id);
                    if (CheckHave10ReportsWith100AvgLetters(ReporterPerson.Id)) 
                    {
                        Initialization.PersonDalIns.UpdateType(ReporterPerson.Id, "potential_agent");
                    }
                }

            }
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
