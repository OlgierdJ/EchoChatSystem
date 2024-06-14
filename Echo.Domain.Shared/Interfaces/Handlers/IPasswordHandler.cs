using Echo.Domain.Shared.Entities.EchoCore.UserCore;

namespace Echo.Domain.Shared.Interfaces.Handlers;

public interface IPasswordHandler
{
    Task<bool> CheckPassword(string Password, SecurityCredentials userPwd);
    Task<SecurityCredentials> CreatePassword(string Password);
    //Task<SecurityCredentials> UpdatePassword(string Password, ulong UserId);
}
