using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerProfileServerRoleConfiguration
    {
        public ulong ProfileId { get; set; }
        public ulong RoleId { get; set; }
        public DateTime TimeGranted { get; set; }
        public ServerProfile Profile { get; set; }
        public ServerRoleConfiguration Role { get; set; }
    }
}
