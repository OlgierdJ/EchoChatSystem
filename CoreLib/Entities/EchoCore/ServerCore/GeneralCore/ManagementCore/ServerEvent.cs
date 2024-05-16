using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore
{
    public class ServerEvent : BaseEntity<ulong>
    {
        public ulong ServerId { get; set; } //server of which the event belongs to
        public ulong CreatorId { get; set; } //person who first made the event (serveradmins might be able to ignore permissions to change event.)
        //public ulong? ServerVoiceChannelId { get; set; } //person who first made the event (serveradmins might be able to ignore permissions to change event.)

        public string Topic { get; set; } //essentially the name of the event
        public string? Description { get; set; } //optional
        public string? ImageFileUrl { get; set; } //optional image
        public EventFrequency EventFrequency { get; set; } //If the event should repeat and if then how often.
        public DateTime StartTime { get; set; } //when does the event start
        public DateTime EndTime { get; set; } //when does the event end.
        public string? Location { get; set; } //text or link to event site.

        //public ServerVoiceChannel? VoiceChannelLocation { get; set; } //if it has location then it shouldnt have voicechannellocation
        public Server Server { get; set; }
        public Account Creator { get; set; } //maybe change owner type and id, cause dont know where to point ownership, directly to account or to the accounts serverprofile.
    }

}