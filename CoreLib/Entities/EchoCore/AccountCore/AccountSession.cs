using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountSession : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public string SessionToken { get; set; } //Token used for verifying calls to the api on the accounts behalf
        public string DeviceId { get; set; } //PC, Windows, Desktop-JHLDPH1235, etc
        public string Location { get; set; } //Denmark, Ballerup, maybe ip, etc
        public DateTime TimeStarted { get; set; } //when session is created
        public DateTime? ExpirationTime { get; set; } //default expiration time when session doesnt work and will logout client (null = permanent session unless revoked)
        public DateTime? TimeStopped { get; set; } //time session has been revoked

        public Account Account { get; set; }
    }
}
