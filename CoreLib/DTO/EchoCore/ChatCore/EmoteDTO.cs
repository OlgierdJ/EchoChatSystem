using CoreLib.Interfaces.Contracts;

namespace CoreLib.DTO.EchoCore.ChatCore;


public class EmoteDTO : IEmote
{
    public ulong Id { get; set; }
    public string Name { get; set; } //this is what you have to type to make the emote. must be atleast 2characters long and only contain alphanumeric characters and underscores?
    public string ImageUrl { get; set; } //actual image. must be jpg, png, gif, and max 256kb, 128x128px
}
