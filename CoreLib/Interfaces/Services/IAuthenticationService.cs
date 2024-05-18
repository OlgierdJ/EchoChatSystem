using CoreLib.DTO.EchoCore.RequestCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.EchoCore.UserCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<string> LoginUserAsync(LoginRequestDTO attempt);
        Task<UserMinimalDTO> RegisterUserAsync(RegisterRequestDTO input);
        Task<UserMinimalDTO> LogoutUserAsync(LoginRequestDTO attempt);
    }
}
