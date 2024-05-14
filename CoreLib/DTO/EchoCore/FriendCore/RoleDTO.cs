using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore
{
    public class RoleDTO
    {
        public ulong Id { get; set; }
        public int OrderingWeight { get; set; } //helps display role by power.
        public string Name { get; set; }
    }
}
