using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel
{
    public class ServerVoiceInvite : IInvite<Account, ulong, Server, ulong>, IDomainEntity
    {
        //maybe extend to allow "guest link" for voicechannel which basically works as the user is allowed into only that specific channel but once they leave they are kicked from the server
        public ulong ChannelId { get; set; } //must have a voicechannel
        public bool GuestInvite { get; set; } //auto-kick when leaving voicecall (wont be implemented at first but maybe later)
        public ServerVoiceChannel Channel { get; set; } //if the invite is to a specific voicechannel
        public string InviteCode { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public int TimesUsed { get; set; }
        public int TotalUses { get; set; }
        public ulong SubjectId { get; set; }
        public ulong InviterId { get; set; }
        public Account Inviter { get; set; }
        public Server Subject { get; set; }
    }
}
