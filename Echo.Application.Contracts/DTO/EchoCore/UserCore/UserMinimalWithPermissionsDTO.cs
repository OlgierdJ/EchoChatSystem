using Echo.Application.Contracts.DTO.Contracts;
using Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore;


public class UserMinimalWithPermissionsDTO : IUserMinimal, IUserMinimalWithPermissions<StatefulPermissionExtendedDTO>
{
    public ICollection<StatefulPermissionExtendedDTO> Permissions { get; set; }
    public string DisplayName { get; set; }
    public ulong Id { get; set; }
    public string ImageIconURL { get; set; }
}
