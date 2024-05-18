using CoreLib.DTO.EchoCore.RequestCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Bases;

namespace CoreLib.Interfaces.Services
{
    public interface IUserService //users personal action servíce for general functionality /*: IEntityService<User, ulong>*/
    {
        Task<User> Register(RegisterRequestDTO requestDTO);
        Task<string> LoginUserAsync(LoginRequestDTO requestDTO);
        Task<bool> UpdatePassword(ulong id, string password);

        Task<bool> AcceptFriendRequest(ulong id, AcceptFriendRequestRequestDTO requestDTO);
        Task<bool> AddFriend(ulong id, AddFriendRequestDTO requestDTO);
        Task<bool> AddUserConnection(ulong id, AddUserConnectionRequestDTO requestDTO);

        Task<bool> BlockUser(ulong id, BlockUserRequestDTO requestDTO);
        Task<bool> MuteUser(ulong id, MuteRequestDTO requestDTO);
        Task<bool> RemoveFriend(ulong id, RemoveFriendRequestDTO requestDTO);
        Task<bool> SetCustomStatus(ulong id, SetStatusRequestDTO requestDTO);
        Task<bool> SetStatus(ulong id, SetStatusRequestDTO requestDTO);
        Task<bool> SetNickname(ulong id, SetNicknameUserRequestDTO requestDTO);
        Task<bool> SetNote(ulong id, SetNoteUserRequestDTO requestDTO);
        Task<bool> SetPhoneNumber(ulong id, EditPhoneNumberRequestDTO requestDTO);
        Task<bool> MuteSelf(ulong id);
        Task<bool> DeafenSelf(ulong id);
        Task<bool> DisableAccount(ulong id, DisableAccountRequestDTO requestDTO);
        Task<bool> DeleteAccount(ulong id, DeleteAccountRequestDTO requestDTO);
    }
}
