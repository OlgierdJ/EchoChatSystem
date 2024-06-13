using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountCustomStatus : BaseEntity<ulong>
{
    //public ulong AccountId { get; set; }
    public string CustomMessage { get; set; } //this will show instead of your actual activity even if the actual activity status message has been disabled in settings but will not show if invisible
    public DateTime? ExpirationTime { get; set; } //null = permanent
    public Account Account { get; set; }
}

