using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IChatService : IEntityService<Chat,ulong>
    {
    }
}
