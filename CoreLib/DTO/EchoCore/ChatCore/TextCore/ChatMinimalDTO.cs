namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public interface IChatMinimal
    {
        ulong Id { get; set; }
        string Name { get; set; }
        string? CategoryName { get; set; }
        int OrderWeight { get; set; }
    }

    public class ChatMinimalDTO : IChatMinimal
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string? CategoryName { get; set; }
        public int OrderWeight { get; set; }
    }
}
