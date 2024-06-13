using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountProfile : BaseEntity<ulong>
{
    public ulong AccountId { get; set; }
    public string DisplayName { get; set; }
    public string AvatarFileURL { get; set; }
    public string BannerColor { get; set; }
    public string? About { get; set; }


    public Account Account { get; set; }

}
