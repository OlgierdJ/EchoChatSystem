namespace CoreLib.DTO.RequestCore.ServerCore.Server;

public class CreateServerRequestDTO
{
    //public ulong UserId { get; set; } //get from jwt
    public string Name { get; set; }
    public bool IsPublic { get; set; }
}
