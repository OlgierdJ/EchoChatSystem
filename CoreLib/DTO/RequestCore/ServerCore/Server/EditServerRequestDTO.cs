using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.Server
{
    public class EditServerRequestDTO
    {
        //public ulong Id { get; set; } //get from jwt
        //public ulong ServerId { get; set; } //get from route param
        public string ServerImageURL { get; set; }
        public string Name { get; set; }
        public ulong InactiveChannelId { get; set; }
        public string InactiveTimeOut { get; set; }
        public ulong SystemMessagesChannelId { get; set; }
        public bool SendRandomWelcomeMessages { get; set; }
        public bool PromptMemberReply { get; set; }
        public bool SendHelpfulTips { get; set; }
        public DefaultNotificationSettings NotificationMode { get; set; }
    }
}
