using CoreLib.Entities.Enums;
using CoreLib.Interfaces.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Hubs
{
    public class DomainEvent
    {
        public string Type { get; set; } //need this for conversion probably
        public string Entity { get; set; }
        public EntityAction Action { get; set; }
    }
    
}
