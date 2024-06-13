namespace Echo.Application.Contracts.DTO.RequestCore.RelationCore;

public class DeafenRequestDTO
{
    //public ulong MuterId { get; set; } //owner from jwt
    public ulong SubjectId { get; set; } //external subject (maybe make id generic, but generally subject always has ulong id.)
}
