using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public interface IPermissionMinimal
    {
        ulong Id { get; set; }
        string Name { get; set; }
    }
    public class PermissionMinimalDTO : IPermissionMinimal
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
    }
}
