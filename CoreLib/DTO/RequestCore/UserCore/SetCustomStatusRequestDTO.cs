namespace CoreLib.DTO.RequestCore.UserCore
{
    public class SetCustomStatusRequestDTO
    {
        //public ulong senderid { get; set; } get from jwt
        //public ulong? ServerId { get; set; } //fk1 to server //just put link in content
        //public ulong? EmoteId { get; set; } //fk2 to emote //just put link in content
        public string? Content { get; set; } //message
        public DateTime? TimeExpires { get; set; } //null = never, else specific time.
        //public byte AssociatedStatusId { get; set; } //not needed? cause it follows further status changes.
    }
}
