using CoreLib.DTO.EchoCore.RequestCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Bases;

namespace CoreLib.Interfaces.Services
{
    public interface IUserService : IEntityService<User, ulong>
    {
        Task<string> LoginUserAsync(string email, string password);
        Task<User> CreateUserAsync(RegisterRequestDTO input);
        Task<bool> UpdatePassword(ulong id, string password);
    }
}
