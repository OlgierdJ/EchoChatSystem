using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.Role
{
    public class SetMultiplePermissionStateRequestDTO
    {
        //public ulong Id { get; set; } //get from jwt
        //public ulong RoleId { get; set; } //get from route param
        public List<Tuple<ulong, bool?>> Permissions { get; set; }
    }
}
