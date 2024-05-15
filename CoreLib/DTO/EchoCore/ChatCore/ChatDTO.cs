using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class ChatDTO //shared between chats, textchannels and such cause they are generally the same
    {
        public ulong Id { get; set; }
        public ulong? LastReadMessageId { get; set; } //from messagetracker used to determine start point when entering chat, which messages to load by pagination. (update locally after first retrieval)
        public string Name { get; set; }
        public int OrderWeight { get; set; }
        //public DateTime TimeLastInteracted { get; set; }
        public PinboardDTO? PinboardDTO { get; set; } //mayb remove and keep decoupled?=?
        //public ICollection<NotificationMessageDTO>? TrackedNotifications { get; set; }
        public ICollection<MemberDTO>? Participants { get; set; } //mapped through chatparticipany or textchannel
        public ICollection<MessageDTO>? Messages { get; set; } //mapped through chatparticipany or textchannel
    }
}
