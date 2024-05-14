namespace CoreLib.DTO.EchoCore.FriendCore.UserCore
{
    public class ActiveActivityStatusDTO
    {
        public byte Id { get; set; } //actual activity status
        public ulong? CustomStatusId { get; set; } //custom text id.
        public string? DisplayedContent { get; set; } //text to display beside status icon. //default activity status name -> overwritten by customstatus message if present.
        public string Icon { get; set; }
        public string IconColor { get; set; }
    }
}