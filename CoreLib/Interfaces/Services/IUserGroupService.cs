﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IUserGroupService
    {
        Task<IEnumerable<string>> GetGroups(ulong accountId);
    }
}
