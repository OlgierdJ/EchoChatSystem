namespace CoreLib.DTO.RequestCore.MessageCore;

public class MarkUnreadRequestDTO //sent to different channel / chat controllers to affect tracked read messages relative to them
{
    //public ulong SenderId { get; set; } from jwt
    public ulong SubjectId { get; set; } //new latest read message
    //public ulong SectionId { get; set; } //chatid, textchannelid, etc whatever is the holder of the message //gotten from message holder of the entity.
}
