using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Bases;

namespace CoreLib.Interfaces.Services
{
    public interface IUserService : IEntityService<User, ulong>
    {
        Task<string> LoginUserAsync(UserLoginModel attempt);
        Task<User> CreateUserAsync(RegisterUserModel input);
        Task<bool> UpdatePassword(UpdatePasswordModel update);
    }
}
