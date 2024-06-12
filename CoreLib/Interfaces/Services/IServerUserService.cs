using CoreLib.DTO.RequestCore.RelationCore;

namespace CoreLib.Interfaces.Services;

public interface IServerUserService //stuff a user can do towards another user in server.
{
    //user stuff
    Task<bool> ServerMute(ulong id, MuteRequestDTO requestDTO);
    Task<bool> ServerDeafen(ulong id, DeafenRequestDTO requestDTO);
    Task<bool> GrantUserRole(ulong id, MuteRequestDTO requestDTO);
    Task<bool> RemoveUserRole(ulong id, MuteRequestDTO requestDTO);
    Task<bool> SetServerNickname(ulong id, MuteRequestDTO requestDTO);
    Task<bool> BanUser(ulong id, MuteRequestDTO requestDTO);
    Task<bool> UnbanUser(ulong id, MuteRequestDTO requestDTO);
    Task<bool> KickUser(ulong id, MuteRequestDTO requestDTO);
    Task<bool> GrantOwnership(ulong id, MuteRequestDTO requestDTO);
    Task<bool> MoveUser(ulong id, MuteRequestDTO requestDTO) => throw new NotSupportedException();

}

