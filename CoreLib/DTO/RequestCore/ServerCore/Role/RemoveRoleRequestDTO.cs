﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.Role
{
    public class RemoveRoleRequestDTO
    {
        //public ulong Id { get; set; } //get from jwt
        public ulong RoleId { get; set; }
    }
}
