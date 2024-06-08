using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.UserCore
{

    public class ActiveActivityStatusDTO : IActivityStatusMinimal, IActiveActivityStatus
        //the displayed status which is a combination of the actual status and the custom status id.

    {
        //public ulong UserId { get; set; } //get user from jwt
        //public byte Id { get; set; } //actual activity status //use userid for update relative?????
        //public ulong? CustomStatusId { get; set; } //custom text id. //find custom status from user.
        public string? DisplayedContent { get; set; } //text to display beside status icon. //default activity status name -> overwritten by customstatus message if present.
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}