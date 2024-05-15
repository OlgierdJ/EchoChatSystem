using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class DeleteAccountRequestDTO
    {
        //public ulong Id { get; set; } get from jwt.
        public string Password { get; set; } //for confirmation
    }
}
