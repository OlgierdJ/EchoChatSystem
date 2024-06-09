using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.UserCore.SettingsCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.Entities.Enums;
using CoreLib.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using CoreLib.Entities.EchoCore.ChatCore;
using Domain.Users;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;
using CoreLib.DTO.Contracts;

namespace CoreLib.Handlers
{
    public partial class SignalRClientService : IDisposable
    {
        private string ServerIP = "https://localhost:7208/PushNotificationHub"; //example "https://localhost:7269/DomainPushNotificationHub"

        public HubConnection Connection { get; set; }

        public Action Disposing { get; set; }

        //need to rework the list to match the list in IPushNotificationHub
        #region list of Action the hub can do
        public event Action<Exception> ConnectionClosed;
        public event Action<Exception> OnConnectedAsync;
        public event Action ConnectionOpened;
        public event Action<UserMinimalDTO> BlockedUserAdded;
        public event Action<ulong> BlockedUserRemoved;
        public event Action<ulong,bool> ChatHiddenStateChanged;
        public event Action<ulong, InviteMinimalDTO> ChatInviteAdded;
        public event Action<ulong,string> ChatInviteRemoved;
        public event Action<ChatDTO> ChatJoined;
        public event Action<ulong, MemberDTO> ChatMemberJoined;
        public event Action<ulong, ulong> ChatMemberLeft;
        public event Action<ulong, bool> ChatMemberOwnershipChanged;
        public event Action<ulong, MessageDTO> ChatMessageAdded;
        public event Action<ulong, ulong > ChatMessageRemoved;
        public event Action<ulong, ulong, string, DateTime?> ChatMessageUpdated;
        public event Action<ulong, bool> ChatMutedStateChanged;
        public event Action<ulong, string, string?> ChatUpdated;
        public event Action<ulong, string> ExternalUserNameChanged;
        public event Action<ulong, string> ExternalUserNoteChanged;
        public event Action<ulong, byte> ExternalUserVolumeChanged;
        public event Action ForceLogout;
        public event Action<UserDTO> FriendAdded;
        public event Action<ulong> FriendRemoved;
        public event Action<FriendRequestDTO> FriendRequestAdded;
        public event Action<RequestType, ulong> FriendRequestRemoved;
        public event Action<ulong, ulong?> ChatLastReadMessageChanged;
        public event Action<ulong> LeftChat;
        public event Action<string, string?> SensitiveDataUpdated;
        public event Action<ulong, byte, string?> UserActivityStatusChanged;
        public event Action<ulong, bool> UserMutedStateChanged;
        public event Action<ulong, string, string, string?, string> UserProfileChanged;
        public event Action<ulong> UserUnmuted;
        public event Action<VoiceSettingsDTO> VoiceSettingsUpdated;
        #endregion

        public SignalRClientService(/*string serverip*/)
        {
            //ServerIP = serverip;
            Disposing = () =>
            {
                Connection?.DisposeAsync();
            };
        }
        public async Task<HubConnection> CreateConnection(string token)
        {
            //Initialize connection
            HubConnection connection = new HubConnectionBuilder()
                      .AddJsonProtocol(a =>
                      {
                          a.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                          //a.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
                      })
                      .WithUrl(ServerIP, o =>
                      {
                          o.AccessTokenProvider = async () => await Task.FromResult<string?>(token);
                          o.Headers.Add("Bearer", token);
                      }).Build();


            //Map events
            
            #region Map over the events
            connection.ServerTimeout = TimeSpan.FromSeconds(2);
            connection.On<UserMinimalDTO>(nameof(IPushNotificationHub.BlockedUserAdded), minimaluser => BlockedUserAdded?.Invoke(minimaluser));
            connection.On<ulong>(nameof(IPushNotificationHub.BlockedUserRemoved), accountId => BlockedUserRemoved?.Invoke(accountId));
            connection.On<ulong, bool>(nameof(IPushNotificationHub.ChatHiddenStateChanged), (chatId, hIdd) => ChatHiddenStateChanged?.Invoke(chatId, hIdd));
            connection.On<ulong, InviteMinimalDTO>(nameof(IPushNotificationHub.ChatInviteAdded), (chatId, minimalinvite) => ChatInviteAdded?.Invoke(chatId, minimalinvite));
            connection.On<ulong, string>(nameof(IPushNotificationHub.ChatInviteRemoved), (chatId, invitecode) => ChatInviteRemoved?.Invoke(chatId, invitecode));
            connection.On<ChatDTO>(nameof(IPushNotificationHub.ChatJoined), chatDTO => ChatJoined?.Invoke(chatDTO));
            connection.On<ulong, MemberDTO>(nameof(IPushNotificationHub.ChatMemberJoined), (chatId, memberDTO) => ChatMemberJoined?.Invoke(chatId, memberDTO));
            connection.On<ulong, ulong>(nameof(IPushNotificationHub.ChatMemberLeft), (chatId, accountId) => ChatMemberLeft?.Invoke(chatId, accountId));
            connection.On<ulong, bool>(nameof(IPushNotificationHub.ChatMemberOwnershipChanged), (accountId, isOwer) => ChatMemberOwnershipChanged?.Invoke(accountId, isOwer));
            connection.On<ulong, MessageDTO>(nameof(IPushNotificationHub.ChatMessageAdded), (chatId, messageDTO) => ChatMessageAdded?.Invoke(chatId, messageDTO));
            connection.On<ulong, ulong>(nameof(IPushNotificationHub.ChatMessageRemoved), (chatId, messageId) => ChatMessageRemoved?.Invoke(chatId, messageId));
            connection.On<ulong, ulong, string, DateTime?>(nameof(IPushNotificationHub.ChatMessageUpdated), (chatId, messageId, content, timeEdited) => ChatMessageUpdated?.Invoke(chatId, messageId, content, timeEdited));
            connection.On<ulong, bool>(nameof(IPushNotificationHub.ChatMutedStateChanged), (chatId, state) => ChatMutedStateChanged?.Invoke(chatId, state));
            connection.On<ulong, string, string?>(nameof(IPushNotificationHub.ChatUpdated), (chatId, name, iconUrl) => ChatUpdated?.Invoke(chatId, name, iconUrl));
            connection.On<ulong, string>(nameof(IPushNotificationHub.ExternalUserNameChanged), (accountId, name) => ExternalUserNameChanged?.Invoke(accountId, name));
            connection.On<ulong, string>(nameof(IPushNotificationHub.ExternalUserNoteChanged), (accountId, note) => ExternalUserNoteChanged?.Invoke(accountId, note));
            connection.On<ulong, byte>(nameof(IPushNotificationHub.ExternalUserVolumeChanged), (accountId, volume) => ExternalUserVolumeChanged?.Invoke(accountId, volume));
            connection.On(nameof(IPushNotificationHub.ForceLogout), () => ForceLogout?.Invoke());
            connection.On<UserDTO>(nameof(IPushNotificationHub.FriendAdded), userDTO => FriendAdded?.Invoke(userDTO));
            connection.On<ulong>(nameof(IPushNotificationHub.FriendRemoved), accountId => FriendRemoved?.Invoke(accountId));
            connection.On<FriendRequestDTO>(nameof(IPushNotificationHub.FriendRequestAdded), friendRequestDTO => FriendRequestAdded?.Invoke(friendRequestDTO));
            connection.On<RequestType, ulong>(nameof(IPushNotificationHub.FriendRequestRemoved), (type, requestId) => FriendRequestRemoved?.Invoke(type, requestId));
            connection.On<ulong, ulong?>(nameof(IPushNotificationHub.ChatLastReadMessageChanged), (chatId, messageId) => ChatLastReadMessageChanged?.Invoke(chatId, messageId));
            connection.On<ulong>(nameof(IPushNotificationHub.LeftChat), chatId => LeftChat?.Invoke(chatId));
            connection.On<string, string?>(nameof(IPushNotificationHub.SensitiveDataUpdated), (email, phoneNumber) => SensitiveDataUpdated?.Invoke(email, phoneNumber));
            connection.On<ulong, byte, string?>(nameof(IPushNotificationHub.UserActivityStatusChanged), (accountId, activityStatusId, customMessage) => UserActivityStatusChanged?.Invoke(accountId, activityStatusId, customMessage));
            connection.On<ulong, bool>(nameof(IPushNotificationHub.UserMutedStateChanged), (accountId, state) => UserMutedStateChanged?.Invoke(accountId, state));
            connection.On<ulong, string, string, string?, string>(nameof(IPushNotificationHub.UserProfileChanged), (accountId, displayName, avatarFileURL, about, bannerColor) => UserProfileChanged?.Invoke(accountId, displayName, avatarFileURL, about, bannerColor));
            connection.On<ulong>(nameof(IPushNotificationHub.UserUnmuted), accountId => UserUnmuted?.Invoke(accountId));
            connection.On<VoiceSettingsDTO>(nameof(IPushNotificationHub.VoiceSettingsUpdated), voiceSettingsDTO => VoiceSettingsUpdated?.Invoke(voiceSettingsDTO));
            #endregion
            
            //Forward invocation of inner event to service event.
            Func<Exception, Task> connectionClosed = async (e) =>
            {
                ConnectionClosed?.DynamicInvoke(e);
            };
            connection.Closed += connectionClosed;
            Disposing += () => { connection.Closed -= connectionClosed; };
            return connection;
        }

        public async Task Disconnect()
        {
            //keep trying until we manage to connect
            while (true)
            {
                try
                {
                    if (Connection != null)
                    {
                        await Connection.StopAsync().ContinueWith(task => {
                            if (task.Exception != null)
                            {
                            }
                        });
                        break; // yay! connected
                    }
                }
                catch (Exception e)
                { /* bugger! */

                }
            }
            return;
        }

        /// <summary>
        /// Attempts to connect to the server with the Connection property.
        /// *NOTE* If the <see cref="Connection"/> is null then a new connection is instantiated.
        /// </summary>
        /// <returns>Task result?</returns>
        public async Task Connect(string token)
        {
            //keep trying until we manage to connect
            while (true)
            {
                try
                {
                    if (Connection == null)
                    {
                        Connection = await CreateConnection(token);
                    }
                    //await Connection.StartAsync();
                    Connection.StartAsync().ContinueWith(task =>
                    {
                        if (task.Exception != null)
                        {
                            ConnectionClosed?.DynamicInvoke(task.Exception);
                        }
                        else
                        {
                            ConnectionOpened?.DynamicInvoke();
                        }
                    });
                    break; // yay! connected
                }
                catch (Exception e)
                { /* bugger! */
                    throw;
                }
            }
            return;
        }

        public async Task SendUpdateMessage(string message)
        {
            await Connection.SendAsync("SendUpdateMessage", message);
        }
        
        public async Task SendAJoinRequestForGroup(string GroupName)
        {
            await Connection.InvokeAsync("JoinGroup",GroupName);
        }

        public void Dispose()
        {
            Disposing();
        }
    }
}
