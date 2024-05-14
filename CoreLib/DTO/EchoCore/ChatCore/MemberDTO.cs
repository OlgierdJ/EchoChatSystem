using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class MemberDTO
    {
        public ulong Id { get; set; } //their unique id for mapping interactions to api.
        public string DisplayName { get; set; } //overwritten by nickname if present
        //public string Name { get; set; } //unique handle
        public string ImageIconURL { get; set; }
        public string NameColour { get; set; } //taken from role or default if no roles.
        public string GroupingName { get; set; } //default members or online depending on chat or server but if serverrole is present then overrides
        public bool IsOwner { get; set; } //if the member owns whatever group they are in.
        public ActiveActivityStatusDTO? ActiveStatus { get; set; } //always needed
        public UserProfileDTO? Profile { get; set; } //lazy load if entering profile from member
    }
}
