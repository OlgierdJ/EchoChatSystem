using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore
{
    public class ServerBan : BaseEntity<ulong> //needs id cause ban can be specific
    {
        public ulong AccountId { get; set; } //who is banned
        public ulong ServerId { get; set; } //where are they banned
        public string? Reason { get; set; } //why are they banned
        public DateTime? ExpirationTime { get; set; } //null = perma //until when are they banned
        public DateTime TimeKeepMessagesBefore { get; set; } //used to determine if and how many messages to delete upon ban.
        public Account Account { get; set; }
        public Server Server { get; set; }
    }
}