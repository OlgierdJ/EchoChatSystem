using CoreLib.Entities.Enums;

namespace CoreLib.DTO.Contracts;

public interface IInviteMinimal
{
    string InviteLink { get; set; }
    InviteType Type { get; set; }
}
