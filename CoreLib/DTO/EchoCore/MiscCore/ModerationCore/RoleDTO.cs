using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class RoleDTO : RoleMinimalDTO
    {
        public ICollection<PermissionExtendedDTO> Permissions { get; set; }
    }
}
