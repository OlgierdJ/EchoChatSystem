using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore
{
    public class Theme : BaseEntity<int>
    {
        public string Name { get; set; }
        public IEnumerable<AccountSettings>? AccountSettings { get; set; }
    }
}
