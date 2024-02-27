using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.UserCore
{
    public class SecurityCredentials : BaseEntity<string>
    {
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
