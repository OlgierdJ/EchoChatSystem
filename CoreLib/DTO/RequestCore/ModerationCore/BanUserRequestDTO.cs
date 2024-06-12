namespace CoreLib.DTO.RequestCore.ModerationCore;

public class BanUserRequestDTO
{
    //public ulong adminid { get; set; } //from jwt
    public ulong UserId { get; set; }
    public string? Reason { get; set; } //why are they banned
    public DateTime? TimeExpired { get; set; } //null = perma //until when are they banned
    public DateTime TimeKeepMessagesBefore { get; set; } //used to determine if and how many messages to delete upon ban.
}
