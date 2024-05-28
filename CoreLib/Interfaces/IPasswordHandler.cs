using CoreLib.Entities.EchoCore.UserCore;

namespace CoreLib.Interfaces
{
    public interface IPasswordHandler
    {
        Task<bool> CheckPassword(string Password, SecurityCredentials userPwd);
        Task<SecurityCredentials> CreatePassword(string Password);
        Task<SecurityCredentials> UpdatePassword(string Password, ulong UserId);
    }
}
