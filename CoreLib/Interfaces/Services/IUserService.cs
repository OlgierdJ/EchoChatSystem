using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Bases;

namespace CoreLib.Interfaces.Services
{
    public interface IUserService //users personal action servíce for general functionality /*: IEntityService<User, ulong>*/
    {
        //friend
        Task<bool> AcceptFriendRequestAsync(ulong senderId, ulong requestId);
        Task<bool> SendFriendRequestAsync(ulong senderId, AddFriendRequestDTO requestDTO);
        Task<bool> SendFriendRequestAsync(ulong senderId, ulong receiverId);
        Task<bool> CancelFriendRequestAsync(ulong senderId, ulong requestId); //yes they are different appearently
        Task<bool> DeclineFriendRequestAsync(ulong senderId, ulong requestId); 
        Task<bool> RemoveFriendAsync(ulong senderId, ulong friendId);


        //other users
        Task<bool> BlockUserAsync(ulong senderId, ulong userId);
        Task<bool> UnblockUserAsync(ulong senderId, ulong userId);
        Task<bool> MuteUserAsync(ulong senderId, ulong userId, MuteRequestDTO requestDTO);
        Task<bool> UnmuteUserAsync(ulong senderId, ulong userId);
        Task<bool> SetNicknameAsync(ulong senderId, ulong userId, SetNicknameUserRequestDTO requestDTO);
        Task<bool> SetNoteAsync(ulong senderId, ulong userId, SetNoteUserRequestDTO requestDTO);
        Task<bool> SetUserVolumeAsync(ulong senderId, ulong userId, SetUserVolumeRequestDTO requestDTO);

        //userself
        Task<bool> AddUserConnectionAsync(ulong senderId, AddUserConnectionRequestDTO requestDTO);
        Task<bool> UpdateUserConnectionAsync(ulong senderId, ulong connectionId, UpdateUserConnectionRequestDTO requestDTO);
        Task<bool> DeafenSelfAsync(ulong senderId);
        Task<bool> UndeafenSelfAsync(ulong senderId);
        Task<bool> DeleteAccountAsync(ulong senderId, DeleteAccountRequestDTO requestDTO);
        Task<bool> DisableAccountAsync(ulong senderId, DisableAccountRequestDTO requestDTO);
        Task<bool> SetPhoneNumberAsync(ulong senderId, EditPhoneNumberRequestDTO requestDTO);
        Task<string> LoginAsync(LoginRequestDTO requestDTO);
        Task<UserFullDTO> LoadUserSessionDataAsync(ulong senderId);
        Task<bool> MuteSelfAsync(ulong senderId);
        Task<bool> UnmuteSelfAsync(ulong senderId);
        Task<bool> RegisterAsync(RegisterRequestDTO requestDTO);
        Task<bool> RemoveUserConnectionAsync(ulong senderId, ulong connectionId);
        Task<bool> SetCustomStatusAsync(ulong senderId, SetCustomStatusRequestDTO requestDTO);
        Task<bool> SetStatusAsync(ulong senderId, SetStatusRequestDTO requestDTO);
        Task<bool> UpdatePasswordAsync(ulong senderId, string password);
        Task<bool> ForgotPasswordAsync(string email, string username);
    }
}
