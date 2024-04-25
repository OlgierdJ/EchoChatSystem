using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Bases;
using CoreLib.Models;

namespace CoreLib.Interfaces.Services
{
    public interface IUserService : IEntityService<User,ulong>
    {
        Task<string> LoginUserAsync(UserLoginModel attempt);
        Task<User> CreateUserAsync(User input, string pword);
    }
}
