namespace Echo.Application.Contracts.DTO.RequestCore.RelationCore;

public class SetUserVolumeRequestDTO //used to change specific user volume via slider
{
    //public ulong SenderId { get; set; } get from jwt
    //public ulong UserId { get; set; } //from route
    public byte Volume { get; set; } //max 200 min 0
}
