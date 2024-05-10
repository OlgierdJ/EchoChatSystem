﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class BillingInformation : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public AccountSettings AccountSettings { get; set; }
        public ICollection<PaymentMethod>? PaymentMethods { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
    }
}
