using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore
{
    public class Device : BaseEntity<int>
    {
        //instance of authorized device with operatingsystem, client application, address, region and country / ip and a timestamp
        public Guid AccountId { get; set; }
        public string Devicetype { get; set; }
        public string Apptype { get; set; }
        public string Region { get; set; }
        public string Conutry { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        //tænker ikke vi har dem her men lave det så kan vi snakke om det
        public string Address { get; set; }
        public string IPAddress { get; set; }
        public Account? Account { get; set; }
    }
}
