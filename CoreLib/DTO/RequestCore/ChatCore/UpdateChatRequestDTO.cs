using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ChatCore
{
    public class UpdateChatRequestDTO
    {
        //public ulong userId { get; set; } //from jwt
        //public ulong chatId { get; set; } //from route
        public string Name { get; set; }
    }
}
