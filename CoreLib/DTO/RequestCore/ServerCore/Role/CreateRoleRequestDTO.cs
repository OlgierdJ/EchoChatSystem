using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.Role
{
    public class CreateRoleRequestDTO
    {
        //public ulong Id {  get; set; } //get from jwt
        public string Name { get; set; } //just default it if you dont want to set it. //not defaulted on serverside
    }
}
