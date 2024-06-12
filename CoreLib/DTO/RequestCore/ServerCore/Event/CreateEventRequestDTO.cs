using CoreLib.Entities.Enums;

namespace CoreLib.DTO.RequestCore.ServerCore;

public class CreateEventRequestDTO
{
    //public ulong Id { get; set; } //user from jwt
    //public ulong serverId { get; set; } //get from route param
    public string? ImageFileUrl { get; set; } //optional image
    public DateTime StartTime { get; set; } //when does the event start
    public DateTime EndTime { get; set; } //when does the event end.
    public string Topic { get; set; } //essentially the name of the event
    public string? Location { get; set; } //text or link to event site.
    public string? Description { get; set; } //optional
    public EventFrequency EventFrequency { get; set; } //If the event should repeat and if then how often. //generate list of possible dates of whenst the next couple of events are started
}
