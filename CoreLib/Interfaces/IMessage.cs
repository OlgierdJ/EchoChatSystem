using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces
{
    public interface IMessage : IEntity
    {
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
