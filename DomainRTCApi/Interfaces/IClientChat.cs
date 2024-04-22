namespace DomainRTCApi.Interfaces
{
    public interface IClientChat
    {
        Task ReceiveMessage(string message);
        Task ReceiveSoundStream(byte[] message);
        Task JoinGroup(string groupName, string clientHandle);
        Task LeaveGroup(string groupName, string clientHandle);
        Task LeaveGroups(string[] groupNames);
        Task StreamToGroup(string groupName, string message);
    }
}
