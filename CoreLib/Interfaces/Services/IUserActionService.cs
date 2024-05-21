using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore.UserCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IUserActionService
    {
        Task<string> LoginUserAsync(LoginRequestDTO attempt);
        Task<User> CreateUserAsync(RegisterRequestDTO input);
        Task<bool> UpdatePassword(ulong id, string password);
    }
}
