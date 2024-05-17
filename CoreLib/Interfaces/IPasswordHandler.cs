using CoreLib.Entities.EchoCore.UserCore;

namespace CoreLib.Interfaces
{
    public interface IPasswordHandler
    {
        Task<bool> CheckPassword(string Password, ulong UserId);
        Task<SecurityCredentials> CreatePassword(string Password, ulong UserId);
        Task<SecurityCredentials> UpdatePassword(string Password, ulong UserId);
    }
}
