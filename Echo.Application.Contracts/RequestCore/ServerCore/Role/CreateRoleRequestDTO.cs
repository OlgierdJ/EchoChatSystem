namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.Role;

public class CreateRoleRequestDTO
{
    //public ulong Id {  get; set; } //get from jwt
    public string Name { get; set; } //just default it if you dont want to set it. //not defaulted on serverside
}
