using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace malshinon.moddels
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecretCode { get; set; }
        public string Type { get; set; }
        public int NumReports { get; set; }
        public int NumMention { get; set; }
        public string Status { get; set; }


        public Person(string FN, string LN, string SC, string type, int NR=0, int NM=0,string status = null, int id = 0 )
        {
            FirstName = FN;
            LastName = LN;
            SecretCode = SC;
            Type = type;
            NumReports = NR;
            NumMention = NM;
            Status = status;
            Id = id; 
        }

        public void PrintAll()
        {
            Console.WriteLine($"id: {Id}. FirstName: {FirstName}. LastName: {LastName} SecretCode: {SecretCode}. Type: {Type}. NumReports : {NumReports}. NumMention : {NumMention}.status : {Status} ");
        }
    }
}
