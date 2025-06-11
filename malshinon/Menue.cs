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
                Console.WriteLine("1. submit a report \n2. disply potential agents \n3. disply dangerous targets \n4. DisplayAllAllertsDaidails \nother key to exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        Initialization.Manage.SubmitReport();
                        break;

                    case "2":
                        Initialization.Manage.DisplyPotentialAgents();
                        break;

                    case "3":
                        Initialization.Manage.DisplyDangerousTargets();
                        break;

                    case "4":
                        Initialization.Manage.DisplayAllAllertsDaidails();
                        break;

                    default:
                        flag = false;
                        break;
                }
            }
        }
            
    }
}
