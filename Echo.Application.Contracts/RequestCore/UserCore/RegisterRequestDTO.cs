namespace Echo.Application.Contracts.DTO.RequestCore.UserCore;

public class RegisterRequestDTO
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool AllowEchoMails { get; set; }
}
