using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.Profile;

public class ReportedProfile : BaseEntity<ulong>
{
    public ulong AccountId { get; set; }
    public string DisplayName { get; set; }
    public string AvatarFileURL { get; set; } //reported profile should duplicate fileurl and store seperately such that the user changing profile avatar does not cause the file to get removed from the fileserver
    public string BannerColor { get; set; }
    public string? About { get; set; }


    public Account Account { get; set; }
    public ProfileReport Report { get; set; }
}
