using CoreLib.DTO.RequestCore.ChatCore;
using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.DTO.RequestCore.ServerCore.ChannelCore;

namespace CoreLib.Interfaces.Services
{
    public interface IChatService
    {
        //Chat
        Task<bool> CreateChat(ulong senderId, ICollection<ulong> startParticipants);
        Task<bool> MuteChat(ulong senderId, ulong chatId, MuteRequestDTO requestDTO);
        Task<bool> UnmuteChat(ulong senderId, ulong chatId);
        Task<bool> HideChat(ulong senderId, ulong chatId);
        Task<bool> AddChatParticipant(ulong senderId, ulong chatId, ulong participantId);
        Task<bool> RemoveChatParticipant(ulong senderId, ulong chatId, ulong participantId);
        Task<bool> AddChatParticipants(ulong senderId, ulong chatId, ICollection<ulong> participantIds);
        Task<bool> DeleteChat(ulong senderId, ulong chatId);
        Task<bool> UpdateChat(ulong senderId, ulong chatId, UpdateChatRequestDTO requestDTO);
        Task<bool> MarkChatAsRead(ulong senderId, ulong chatId);
        Task<bool> SetChatImage(ulong senderId, ulong chatId, SetImageRequestDTO requestDTO); //uploads and uses only serverimage
        Task<bool> LeaveChat(ulong senderId, ulong chatId);
        //message
        Task<bool> SendDirectMessageMessage(ulong senderId, ulong receiverId, SendMessageRequestDTO requestDTO);
        Task<bool> SendChatMessage(ulong senderId, ulong chatId, SendMessageRequestDTO requestDTO);
        Task<bool> RemoveChatMessage(ulong senderId, ulong chatId, ulong messageId);
        Task<bool> PinChatMessage(ulong senderId, ulong chatId, ulong messageId);
        Task<bool> UpdateChatMessage(ulong senderId, ulong chatId, ulong messageId, EditMessageRequestDTO requestDTO);
        Task<bool> UnpinChatMessage(ulong senderId, ulong chatId, ulong messageId);

        

        //invite
        /// <summary>
        /// verify user permission to invite (either owner or explicit permission),
        /// create invite,
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> CreateChatInvite(ulong senderId, ulong chatId, CreateInviteRequestDTO requestDTO);
        /// <summary>
        /// verify invite still exists and user is allowed to join (if they are not a part of the thing already and are not banned),
        /// consume invite updating its usecount, 
        /// create serverprofile linking join method,
        /// default roles (everyone), 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> ConsumeChatInvite(ulong senderId, ulong chatId, string inviteCode);
        /// <summary>
        /// verify user permission to delete / edit invites, 
        /// softdelete invite, 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> RemoveChatInvite(ulong senderId, ulong chatId, string inviteCode) => throw new NotSupportedException();
    }

}
