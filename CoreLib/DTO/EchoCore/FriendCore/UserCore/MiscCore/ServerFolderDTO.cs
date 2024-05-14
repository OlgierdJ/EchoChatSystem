using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.MiscCore
{
    public class ServerFolderDTO
    {
        public ulong Id { get; set; }
        public int Importance { get; set; } //used for ordering folders
        public string Name { get; set; } //name of the folder
        public string? Colour { get; set; } //null = default, else set in hex string

    }
}
