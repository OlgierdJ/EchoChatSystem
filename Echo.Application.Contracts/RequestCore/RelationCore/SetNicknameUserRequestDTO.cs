namespace Echo.Application.Contracts.DTO.RequestCore.RelationCore;

public class SetNicknameUserRequestDTO //adds personal nickname on user
{
    //public ulong SenderId { get; set; } from jwt
    //public ulong UserId { get; set; }
    public string Nickname { get; set; }
}
