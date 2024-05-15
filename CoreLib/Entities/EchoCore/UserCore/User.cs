﻿using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.UserCore
{
    public class User : BaseEntity<ulong>
    {
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime PasswordSetDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Account? Account { get; set; }
        public SecurityCredentials? SecurityCredentials { get; set; }
    }
}
