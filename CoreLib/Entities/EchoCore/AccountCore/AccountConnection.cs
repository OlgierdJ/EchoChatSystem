using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountConnection : BaseEntity<ulong> //mayb review
    {
        public ulong AccountId { get; set; }
        public uint ConnectionId { get; set; }

        public bool DisplayOnProfile { get; set; }
        public string Name { get; set; } //platform user name
        public string AuthorizeResponseType { get; set; }
        public string AuthorizeClientId { get; set; }
        public string AuthorizeState { get; set; }
        public string AuthorizeCodeChallenge { get; set; }

        public string ExternalRefreshToken { get; set; }
        public string InternalRefreshToken { get; set; }

        public Account Account { get; set; }
        public Connection Connection { get; set; }
    }
}