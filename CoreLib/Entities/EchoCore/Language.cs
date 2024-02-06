﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore
{
    public class Language : BaseEntity<int>
    {
        public ulong AccountId { get; set; }
        public string Conutry { get; set; }
        public byte[] ConutryFlag { get; set; }
        public Account? Account { get; set; }
    }
}
