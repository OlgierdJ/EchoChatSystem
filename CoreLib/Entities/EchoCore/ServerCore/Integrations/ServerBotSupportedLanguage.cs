using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations
{
    public class ServerBotSupportedLanguage : BaseEntity<ulong>
    {
        public ulong LanguageId { get; set; }
        public ulong ServerBotId { get; set; }
        public ServerBot ServerBot { get; set; }
        public Language Language { get; set; }
    }
}
