using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class Language : BaseEntity<int>
    {
        //her lave det om til at NotificationSettings kigger på AccountSettings i stedefor a kigge Account
        public ulong AccountSettingsId { get; set; }
        public string Conutry { get; set; }
        public byte[] ConutryFlag { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}
