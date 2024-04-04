using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountActivityStatus : BaseEntity<byte> //maybe review
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
