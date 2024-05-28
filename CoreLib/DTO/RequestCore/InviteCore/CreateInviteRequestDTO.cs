using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.InviteCore
{
    public class CreateInviteRequestDTO
    {
        //public ulong Id { get; set; } //get from jwt
        //public ulong SubjectId { get; set; } //get from route param
        public DateTime? TimeExpires { get; set; }
        public int TotalUses { get; set; }
        public bool TemporaryMembership { get; set; }
    }
}
