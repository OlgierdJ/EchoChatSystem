using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ServerCore.Integrations
{
    public class ServerBotCommand //: BaseEntity<ulong>
    {
        public string Name { get; set; }
        public string CommandURL { get; set; }
        public string? Description { get; set; }
        public ICollection<ServerBotCommandParameter> Parameters { get; set; }
        public ICollection<ServerBotIntegrationCommandRoleOverride> CommandRoleOverrides { get; set; }
        public ICollection<ServerBotIntegrationCommandMemberOverride>  CommandMemberOverrides { get; set; }
        public ICollection<ServerBotIntegrationCommandTextChannelOverride>  CommandTextChannelOverrides { get; set; }
        public ICollection<ServerBotIntegrationCommandVoiceChannelOverride>  CommandVoiceChannelOverrides { get; set; }
    }
}
