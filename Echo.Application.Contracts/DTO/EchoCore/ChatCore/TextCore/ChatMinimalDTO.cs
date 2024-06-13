using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.ChatCore.TextCore;


public class ChatMinimalDTO : IChatMinimal
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string? CategoryName { get; set; }
    public int OrderWeight { get; set; }
}
