namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.Role;

public class SetMultiplePermissionStateRequestDTO
{
    //public ulong Id { get; set; } //get from jwt
    //public ulong RoleId { get; set; } //get from route param
    public List<Tuple<ulong, bool?>> Permissions { get; set; }
}
