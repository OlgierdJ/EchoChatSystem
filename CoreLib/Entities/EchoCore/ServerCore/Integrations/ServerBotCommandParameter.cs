using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations
{
    public class ServerBotCommandParameter : BaseEntity<ulong>
    {
        public ulong ServerBotCommandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public uint Importance { get; set; }
        public ICollection<ServerBotCommandParameterValue>  ParameterValues { get; set; }
    }
}