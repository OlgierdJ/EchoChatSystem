namespace CoreLib.DTO.EchoCore.ServerCore
{
    public interface IServerMinimal
    {
        ulong Id { get; set; }
        string ImageIconURL { get; set; }
        string Name { get; set; }
    }

    public class ServerMinimalDTO : IServerMinimal
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string ImageIconURL { get; set; }
    }
}
