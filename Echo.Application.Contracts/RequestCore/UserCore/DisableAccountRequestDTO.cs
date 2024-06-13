namespace Echo.Application.Contracts.DTO.RequestCore.UserCore;

public class DisableAccountRequestDTO
{
    //public ulong Id { get; set; } get from jwt.
    public string Password { get; set; } //for confirmation
}
