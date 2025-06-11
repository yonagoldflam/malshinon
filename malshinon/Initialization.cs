using malshinon.dal;
using malshinon.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    public static class Initialization
    {
        public static PeopleDal PersonDalIns = new PeopleDal();
        public static IntelReportsDal IntelReportDalIns = new IntelReportsDal();
        
        public static MySqlData SqlData = new MySqlData(); 
        public static Manager Manage = new Manager();
    }
}
