using CoreLib.Entities.Enums;

namespace CoreLib.DTO.RequestCore.InviteCore;

public class CreateInviteRequestDTO
{
    //public ulong Id { get; set; } //get from jwt
    //public ulong SubjectId { get; set; } //get from route param
    public ulong? ChannelId { get; set; } // landing page basically
    public DateTime? TimeExpires { get; set; }
    public InviteType Type { get; set; }
    public int TotalUses { get; set; }
    public bool TemporaryMembership { get; set; }
}
