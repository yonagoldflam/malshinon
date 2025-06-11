using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon.moddels
{
    public class Alert
    {
        public int Id {  get; set; }
        public int TargetId { get; set; }
        public string CreatedAt { get; set; }
        public string Reason { get; set; }
        public Alert(int TI, string reason, int id = 0, string CT= null) 
        {
            TargetId = TI;    
            Id = id;
            CreatedAt = CT;
            Reason = reason;           
        }
    }
}
