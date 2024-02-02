using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountProfile : BaseEntity<ulong>
    {
        public string AccountId { get; set; }
        public string DisplayName { get; set; }
        public string AvatarFileURL { get; set; }
        public string? About { get; set; }

        public Account Account { get; set; }
    }
}
