using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ReportCore.Bug;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Feedback;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Hubs
{
    public interface IPushNotificationHub
    {
        Task ReceiveMemberDTOCreateMessage(MemberDTO entity);
        Task ReceiveMemberDTOUpdateMessage(MemberDTO entity);
        Task ReceiveMemberDTODeleteMessage(MemberDTO entity);

        Task ReceiveUserMinimalDTOCreateMessage(UserMinimalDTO entity);
        Task ReceiveUserMinimalDTOUpdateMessage(UserMinimalDTO entity);
        Task ReceiveUserMinimalDTODeleteMessage(UserMinimalDTO entity);

        Task ReceiveUserProfileDTOCreateMessage(UserProfileDTO entity);
        Task ReceiveUserProfileDTOUpdateMessage(UserProfileDTO entity);
        Task ReceiveUserProfileDTODeleteMessage(UserProfileDTO entity);

        Task ReceiveUserFullDTOCreateMessage(UserFullDTO entity);
        Task ReceiveUserFullDTOUpdateMessage(UserFullDTO entity);
        Task ReceiveUserFullDTODeleteMessage(UserFullDTO entity);

        Task ReceiveUserDTOCreateMessage(UserDTO entity);
        Task ReceiveUserDTOUpdateMessage(UserDTO entity);
        Task ReceiveUserDTODeleteMessage(UserDTO entity);
    }
}
