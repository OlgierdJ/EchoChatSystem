using System.Collections.Concurrent;

namespace DomainRTCApi
{
    public class ChatHubClientManager
    {
        public ConcurrentDictionary<string, string> ClientConnectionMappings { get; set; } //maps clienthandles to connections
        public ConcurrentDictionary<string, bool> IgnoreDCEventList { get; set; } //handles clientconnections to ignore on dc
    }
}
