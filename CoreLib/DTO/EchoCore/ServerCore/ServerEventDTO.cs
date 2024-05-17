using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerEventDTO
    {
        public ulong Id { get; set; } //id of event
        public string? ImageFileUrl { get; set; } //optional image
        public DateTime StartTime { get; set; } //when does the event start
        public DateTime EndTime { get; set; } //when does the event end.
        public string Topic { get; set; } //essentially the name of the event
        public ServerMinimalDTO Server { get; set; }
        public string? Location { get; set; } //text or link to event site.
        public ICollection<UserDTO>? InterestedUsers { get; set; }
        public UserDTO Creator { get; set; } //person who first made the event
        public string? Description { get; set; } //optional
        public EventFrequency EventFrequency { get; set; } //If the event should repeat and if then how often. //generate list of possible dates of whenst the next couple of events are started
    }
}
