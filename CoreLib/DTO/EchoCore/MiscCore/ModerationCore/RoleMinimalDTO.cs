using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class RoleMinimalDTO : IRoleMinimal
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
    }
}
