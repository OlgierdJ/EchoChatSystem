using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.FriendCore
{
    public class BlockedUserDTO
    {
        public ulong Id { get; set; } //their unique id for mapping interactions to api.
        public string DisplayName { get; set; } //unique handle or displayname if present.
        public string ImageIconURL { get; set; }
    }
}
