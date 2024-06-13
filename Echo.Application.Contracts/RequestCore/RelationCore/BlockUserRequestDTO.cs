namespace Echo.Application.Contracts.DTO.RequestCore.RelationCore;

public class BlockUserRequestDTO //blocks user
{
    //public ulong BlockerId { get; set; } //owner from jwt
    public ulong UserId { get; set; } //external user
}
