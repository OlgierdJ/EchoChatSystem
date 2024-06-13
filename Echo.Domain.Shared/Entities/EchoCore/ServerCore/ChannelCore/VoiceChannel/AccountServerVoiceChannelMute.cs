using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;

public class AccountServerVoiceChannelMute : ITargetedMute<Account, ulong, ServerVoiceChannel, ulong>, IDomainEntity
{
    public ulong SubjectId { get; set; }
    public ulong MuterId { get; set; }
    public DateTime TimeMuted { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public Account Muter { get; set; }
    public ServerVoiceChannel Subject { get; set; }
}
