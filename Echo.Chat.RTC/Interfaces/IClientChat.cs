namespace DomainRTCApi.Interfaces;

public interface IClientChat
{
    Task ReceiveMessage(string message);
    Task ReceiveSoundStream(byte[] message);
    Task ParticipantJoinedGroup(string groupName, string clientHandle);
    Task ParticipantLeftGroup(string groupName, string clientHandle);
    Task KillConnection();
    Task LeaveGroups(string[] groupNames);
    Task StreamToGroup(string groupName, string message);
}
