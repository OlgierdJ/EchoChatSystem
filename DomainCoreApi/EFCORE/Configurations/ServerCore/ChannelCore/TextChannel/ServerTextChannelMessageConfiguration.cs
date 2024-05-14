using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelMessageConfiguration : BaseMessage<ulong, Account, ulong, ServerTextChannelConfiguration, ulong, ServerTextChannelMessageConfiguration>
    {
        public ICollection<ServerTextChannelMessageAttachmentConfiguration>? Attachments { get; set; }
        public ChatMessagePin? MessagePin { get; set; }
        public ICollection<ServerTextChannelAccountMessageTrackerConfiguration>? MessageTrackers { get; set; }
    }
}
