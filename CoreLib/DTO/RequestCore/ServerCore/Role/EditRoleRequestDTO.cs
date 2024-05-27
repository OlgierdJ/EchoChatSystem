using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.Role
{
    public class EditRoleRequestDTO
    {
        //public ulong Id { get; set; } //get from jwt
        public ulong RoleId { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public int Importance { get; set; } //hierarchy
        public string? ImageURL { get; set; }
        public bool DisplaySeperatelyFromOnline { get; set; }
        public bool AllowAnyoneMention { get; set; }
    }
}
