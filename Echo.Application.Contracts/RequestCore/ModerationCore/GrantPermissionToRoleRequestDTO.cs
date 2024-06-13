namespace Echo.Application.Contracts.RequestCore.ModerationCore;

public class GrantPermissionToRoleRequestDTO //sent to different controllers such as applicationrole, serverrole,
                                             //to grant permissions to a role if they do not already have it or alter the state of the permission if they have it.
{
    //public ulong UserId { get; set; } //get from jwt
    public ulong RoleId { get; set; } //permissionid
    public ulong PermissionId { get; set; } //permissionid
    public bool? State { get; set; }
}
