using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerProfileDTO //used for displaying profile to user and updating upon changing and saving
    {
        //public ulong Id { get; set; } //fk1 to user //get from jwt on update
        public ulong ServerId { get; set; } //fk2 to server
        public string Nickname { get; set; }
        public string BannerColour { get; set; }
        public string ImageIconURL { get; set; }
        public string? AboutMe { get; set; }
    }
}
