namespace Echo.Application.Contracts.RequestCore.ChatCore;

public class UpdateChatRequestDTO
{
    //public ulong userId { get; set; } //from jwt
    //public ulong chatId { get; set; } //from route
    public string Name { get; set; }
}
