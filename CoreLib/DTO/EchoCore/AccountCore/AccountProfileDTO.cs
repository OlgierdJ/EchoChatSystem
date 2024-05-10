﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.AccountCore
{
    public class AccountProfileDTO : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public string DisplayName { get; set; }
        public string AvatarFileURL { get; set; }
        public string BannerColor { get; set; }
        public string? About { get; set; }

    }
}