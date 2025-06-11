using malshinon.moddels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    public class Manager
    {
        public Person ReporterPerson;
        public Person TargetPerson;
        public void SubmitReport()
        {
            Console.WriteLine("enter your secret code");
            string ReporterSecretCode = Console.ReadLine();
            Console.WriteLine("enter target secret code");
            string TargetSecretCode = Console.ReadLine();
            Console.WriteLine("enter report text");
            string Text = Console.ReadLine();

            ReporterPerson = Initialization.PersonDalIns.GetPersonBySecretCode(ReporterSecretCode);
            ReporterPerson = CheckIfPersonNullAndCreateNewPerson(ReporterPerson, ReporterSecretCode, "reporter");

            TargetPerson = Initialization.PersonDalIns.GetPersonBySecretCode(TargetSecretCode);            
            TargetPerson = CheckIfPersonNullAndCreateNewPerson(TargetPerson, TargetSecretCode, "target");

            bool Done = Initialization.IntelReportDalIns.Report(ReporterPerson.Id, TargetPerson.Id, Text);
            if (Done) 
            {
                HandlePostReportUpdates();
            }
        }

        
        public Person CheckIfPersonNullAndCreateNewPerson(Person person, string SecretCode, string PersonType)
        {
            if (person == null)
            {
                Console.WriteLine("You are identified as a new user in the system, please enter your details: ");
                person = Initialization.PersonDalIns.CreatePerson(SecretCode, PersonType);
                Initialization.PersonDalIns.AddPerson(person);
                person = Initialization.PersonDalIns.GetPersonBySecretCode(SecretCode);
            }
            return person;
        }
        
        public void HandlePostReportUpdates()
        {
            Initialization.PersonDalIns.UpdateNumReports(ReporterPerson.Id);
            Initialization.PersonDalIns.UpdateNumMentions(TargetPerson.Id);
            if (Initialization.IntelReportDalIns.CalculateAvaregeLengthMeseges(ReporterPerson.Id) >= 100)
                Initialization.PersonDalIns.UpdateType(ReporterPerson.Id, "potential_agent");

            if (Initialization.PersonDalIns.IsDangerous(TargetPerson.Id))
                Initialization.PersonDalIns.UpdateStatus(TargetPerson.Id);
        }

        public void DisplyDangerousTargets()
        {
            int DangerousTargetId =Initialization.PersonDalIns.PrintDangerousTargets();
        }

        public void DisplyPotentialAgents()
        {
            int DangerousTargetId = Initialization.PersonDalIns.DisplyPotentialAgent();
            Console.WriteLine($" Average message length: {Initialization.IntelReportDalIns.CalculateAvaregeLengthMeseges(DangerousTargetId)} ");
        }

    }
}
