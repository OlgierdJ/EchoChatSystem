using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class PaymentType : BaseEntity<uint>
    {
        public string Name { get; set; }
        public ICollection<PaymentMethod>? PaymentMethods { get; set; }
    }
}
