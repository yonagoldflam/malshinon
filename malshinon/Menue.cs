using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    public static class Menue
    {

        public static void PrintMenue()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Choose one of the following options:");
                Console.WriteLine("1. submit a report\n");
                switch (Console.ReadLine())
                {
                    case "1":
                        Initialization.Manage.SubmitReport();
                        break;

                    default:
                        flag = false;
                        break;
                }
            }
        }
            
    }
}
