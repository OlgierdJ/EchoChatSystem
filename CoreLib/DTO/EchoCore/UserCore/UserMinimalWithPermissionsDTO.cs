using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserMinimalWithPermissionsDTO : UserMinimalDTO
    {
        public ICollection<PermissionExtendedDTO>  Permissions { get; set; }
    }
}
