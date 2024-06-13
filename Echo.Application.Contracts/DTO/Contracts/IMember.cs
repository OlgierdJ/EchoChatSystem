using Echo.Application.Contracts.DTO.EchoCore.UserCore;

namespace Echo.Application.Contracts.DTO.Contracts;

public interface IMember
{
    ActiveActivityStatusDTO? ActiveStatus { get; set; }
    string GroupingName { get; set; }
    bool IsOwner { get; set; }
    string NameColour { get; set; }
    ExternalUserProfileDTO? Profile { get; set; }
}
