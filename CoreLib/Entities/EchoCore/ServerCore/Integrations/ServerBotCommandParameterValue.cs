using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations;

public class ServerBotCommandParameterValue : BaseEntity<ulong>
{
    public ulong ServerBotCommandParameterId { get; set; }
    public ServerBotCommandParameter ServerBotCommandParameter { get; set; }
    public string ValueType { get; set; }
    public string Value { get; set; }
    public string MinRange { get; set; }
    public string MaxRange { get; set; }
}