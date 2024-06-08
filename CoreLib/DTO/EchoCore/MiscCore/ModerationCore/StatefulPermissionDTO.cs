using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{

    public class StatefulPermissionDTO : IStatefulPermission
    {
        public ulong Id { get; set; }
        public string Name { get; set; } //Example app_view_admin_userinterface
        public bool? State { get; set; } //state. (true = enabled, null = default, false = disabled)
    }
}
