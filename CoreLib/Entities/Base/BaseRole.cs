using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseRole<TId, TRecipient, TPermission> : BaseEntity<TId>
    {
        public string Name { get; set; }
        public int Importance { get; set; }

        public List<TRecipient>? Recipients { get; set; }
        public List<TPermission>? Permissions { get; set; }
    }
}
