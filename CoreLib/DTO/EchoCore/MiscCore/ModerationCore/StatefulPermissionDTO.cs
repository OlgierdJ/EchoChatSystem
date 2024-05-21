using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public interface IStatefulPermission : IPermissionMinimal
    {
        ulong Id { get; set; }
        string Name { get; set; }
        bool? State { get; set; }
    }

    public class StatefulPermissionDTO : IStatefulPermission
    {
        public ulong Id { get; set; }
        public string Name { get; set; } //Example app_view_admin_userinterface
        public bool? State { get; set; } //state. (true = enabled, null = default, false = disabled)
    }
}
