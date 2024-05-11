using CoreLib.Entities.EchoCore.ServerCore;
using CoreLib.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IServerService : IEntityService<Server,ulong>
    {
    }
}
