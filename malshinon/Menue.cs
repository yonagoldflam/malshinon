using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    public class Menue
    {
        public Menue()
        {

        }

        public void PrintMenue()
        {
            Console.WriteLine("Choose one of the following options:");
            Console.WriteLine("1. submit a report");
            switch (Console.ReadLine())
            {
                case "1":
                    break;
            }
        }
            
    }
}
