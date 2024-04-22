using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Bases;

namespace CoreLib.Interfaces.Services
{
    public interface IUserService : IEntityService<User,ulong>
    {
        Task<User> LoginUserAsync(UserLogins attempt);
        Task<User> CreateUserAsync(User input, string pword);
    }
}
