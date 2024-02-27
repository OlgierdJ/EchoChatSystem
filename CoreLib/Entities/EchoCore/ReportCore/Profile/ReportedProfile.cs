using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ReportCore.Profile
{
    public class ReportedProfile : BaseEntity<ulong>
    {
        //not yet implemented
        public ulong AccountId { get; set; }
        public string DisplayName { get; set; }
        public string AvatarFileURL { get; set; }
        public string BannerColor { get; set; }
        public string? About { get; set; }


        public Account Account { get; set; }
    }
}
