using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore
{
    public class ServerVoiceMute : ITargetedMute<Server, ulong, Account, ulong>,IDomainEntity //serverwide mute overrides accountmute relation
    {
        //maybe put bool flag in serverprofile?
        public ulong SubjectId { get; set; }
        public ulong MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public Server Muter { get; set; }
        public Account Subject { get; set; }
    }
}
