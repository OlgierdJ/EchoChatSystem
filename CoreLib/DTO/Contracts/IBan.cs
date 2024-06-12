namespace CoreLib.DTO.Contracts;

public interface IBan
{
    DateTime? ExpirationTime { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    string? Reason { get; set; }

}

public interface IBan<TUser> : IBan
{
    TUser User { get; set; }
}
