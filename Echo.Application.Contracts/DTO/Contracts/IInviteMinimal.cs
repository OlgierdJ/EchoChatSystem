using Echo.Application.Contracts.Enums;

namespace Echo.Application.Contracts.DTO.Contracts;

public interface IInviteMinimal
{
    string InviteLink { get; set; }
    InviteType Type { get; set; }
}
