using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class UserConnectionDTO
    {
        //public ulong UserId { get; set; } //get from jwt
        public string Name { get; set; } //connection account name
        public bool DisplayOnProfile { get; set; } //shows badge on userprofile.
        public ConnectionDTO Type { get; set; }
    }
}
