namespace DomainPushNotificationApi.Services
{
    public class PushNotificationClientConnectionStore
    {
        //used for accessing connections from outside hub so that we can add users to groups without making a roundtrip to the client.
        public Dictionary<ulong, List<string>> UserConnectionMappings { get; set; } = new(); 

        public void ClearUserConnections(ulong accountId)
        {
            UserConnectionMappings.Remove(accountId);
        }
        public void RemoveUserConnection(ulong accountId, string connectionId)
        {
            if (UserConnectionMappings.TryGetValue(accountId, out var list))
            {
                list.Remove(connectionId);
            }
        }

        public void AddUserConnection(ulong accountId, string connectionId)
        {
            var exists = UserConnectionMappings.TryGetValue(accountId, out var list);
            if (!exists)
            {
                UserConnectionMappings.Add(accountId, new List<string>());
            }
            var currentList = UserConnectionMappings[accountId]; //we know it exists by this point
            currentList.Add(connectionId);
        }
    }
}
