using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore
{
    public class FriendDTO
    {
        public ulong Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public ActivityStatusDTO Status { get; set; }
    }
}
