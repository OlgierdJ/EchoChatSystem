using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.Contracts;

public interface IUser
{
    ActiveActivityStatusDTO ActiveStatus { get; set; }
    string Name { get; set; }
}
