using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountBadge : BaseEntity<byte>
{
    public ulong AccountId { get; set; } //combi key with id and accountid
    public string Description { get; set; } //written content example "Subscriber since 26 may 2019"
    public string IconURL { get; set; } //example "ActiveSubscriberBadgeIcon.png"
    public Account Account { get; set; } //combi key with id and accountid
}
