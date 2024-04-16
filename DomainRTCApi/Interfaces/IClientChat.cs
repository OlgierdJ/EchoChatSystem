﻿namespace DomainRTCApi.Interfaces
{
    public interface IClientChat
    {
        Task ReceiveMessage(string message);
        Task ReceiveSoundStream(string message);
        Task JoinGroup(string groupName);
        Task LeaveGroup(string groupName);
        Task LeaveGroups(string[] groupNames);
        Task StreamToGroup(string groupName, string message);
    }
}
