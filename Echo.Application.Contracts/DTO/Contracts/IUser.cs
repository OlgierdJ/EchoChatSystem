using Echo.Application.Contracts.DTO.EchoCore.UserCore;

namespace Echo.Application.Contracts.DTO.Contracts;

public interface IUser
{
    ActiveActivityStatusDTO ActiveStatus { get; set; }
    string Name { get; set; }
}
