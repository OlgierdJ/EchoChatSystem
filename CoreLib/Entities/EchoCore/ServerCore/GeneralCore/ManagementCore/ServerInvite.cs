using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore
{
    public class ServerInvite : IInvite<Account, ulong, Server, ulong>, IDomainEntity
    {
        public bool GuestInvite { get; set; } //kick from server on voice leave or kick from server when disconnecting from discord??
        public ulong? TextChannelId { get; set; } //if no textchannel is specified the invite still works to the server
        public ServerTextChannel? TextChannel { get; set; } //if the invite is to a specific textchannel
        public ulong? VoiceChannelId { get; set; } //if no textchannel is specified the invite still works to the server
        public ServerVoiceChannel? VoiceChannel { get; set; } //if the invite is to a specific textchannel
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
