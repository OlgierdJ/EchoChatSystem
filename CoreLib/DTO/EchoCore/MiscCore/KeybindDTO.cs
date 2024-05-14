using CoreLib.Entities.EchoCore.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class KeybindDTO
    {
        public ulong Id { get; set; } //account owner
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; } //example: ALT + i
    }
}
