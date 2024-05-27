using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.MessageCore
{
    public class MarkReadRequestDTO
    {
        //public ulong Id { get; set; } //user from jwt
        public ulong SubjectId { get; set; }
    }
}
