namespace CoreLib.DTO.RequestCore.ServerCore.Role;

public class SetPermissionStateRequestDTO
{
    //public ulong Id { get; set; } //get from jwt
    //public ulong RoleId { get; set; } //get from route param
    public ulong PermissionId { get; set; }
    public bool? State { get; set; }
}
