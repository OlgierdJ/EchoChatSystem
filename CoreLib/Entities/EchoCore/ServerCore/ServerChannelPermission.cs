using CoreLib.Entities.Base;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class ServerChannelPermission : BaseEntity<int>
    {
        public string Name { get; set; }
        public string PermissionCategory { get; set; } //used for grouping permissions together
        public string Description { get; set; }
        //public PermissionState ToggleState { get; set; }
    }
}
