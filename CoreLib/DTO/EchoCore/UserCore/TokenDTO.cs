﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; } //store this in a safe place
    }
}
