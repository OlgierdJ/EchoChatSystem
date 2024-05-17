using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool AllowEchoMails { get; set; }
    }
}
