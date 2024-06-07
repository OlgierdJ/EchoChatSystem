using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.RequestCore.MessageCore;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLib.Handlers
{
    public partial class SignalRClientService : IDisposable
    {
        private string ServerIP = "https://localhost:7269/DomainPushNotificationHub"; //example "https://localhost:7269/DomainPushNotificationHub"

        public HubConnection Connection { get; set; }

        public Action Disposing { get; set; }

        #region list of Action the hub can do
        public event Action<Exception> ConnectionClosed;
        public event Action<Exception> OnConnectedAsync;
        public event Action<string> JoinGroup;
        public event Action<string[]> JoinGroups;
        public event Action<string> LeaveGroup;
        public event Action<string[]> LeaveGroups;
        public event Action<string> UpdateMessageReceived;
        public event Action<string> ReceiveNotification;
        public event Action<MessageDTO> ReceiveChatMessageCreateMessageDTO;
        public event Action<MessageDTO> ReceiveChatMessageUpdateMessageDTO;
        public event Action<MessageDTO> ReceiveChatMessageDeleteMessageDTO;
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
                      //.AddJsonProtocol(a =>
                      //{
                      //    a.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                      //    //a.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
                      //})
                      .WithUrl(ServerIP, o =>
                      {
                          //o.UseDefaultCredentials = true;
                          o.AccessTokenProvider = async () => await Task.FromResult<string?>(token);
                          o.Headers.Add("Bearer", token);
                      }).Build();


            //Map events

            connection.ServerTimeout = TimeSpan.FromSeconds(2);
            //connection.On<string>(nameof(IDomainNotificationHub.ReceiveUpdateMessage), (message) => UpdateMessageReceived?.Invoke(message));
            //connection.On<string>(nameof(IDomainNotificationHub.ReceiveNotification), (message) => ReceiveNotification?.Invoke(message));
            //connection.On<string>(nameof(IDomainNotificationHub.JoinGroup), (groupName) => JoinGroup?.Invoke(groupName));
            //connection.On<string[]>(nameof(IDomainNotificationHub.JoinGroups), (groupNames) => JoinGroups?.Invoke(groupNames));
            //connection.On<string>(nameof(IDomainNotificationHub.LeaveGroup), (groupNames) => LeaveGroup?.Invoke(groupNames));
            //connection.On<string[]>(nameof(IDomainNotificationHub.LeaveGroups), (groupNames) => LeaveGroups?.Invoke(groupNames));
            //connection.On<MessageDTO>(nameof(IDomainNotificationHub.ReceiveChatMessageCreateMessageDTO), (message) => ReceiveChatMessageCreateMessageDTO?.Invoke(message));
            //connection.On<MessageDTO>(nameof(IDomainNotificationHub.ReceiveChatMessageUpdateMessageDTO), (message) => ReceiveChatMessageUpdateMessageDTO?.Invoke(message));
            //connection.On<MessageDTO>(nameof(IDomainNotificationHub.ReceiveChatMessageDeleteMessageDTO), (message) => ReceiveChatMessageDeleteMessageDTO?.Invoke(message));
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
                    await Connection.StartAsync();
                    //Connection.StartAsync().ContinueWith(task => {
                    //    if (task.Exception != null)
                    //    {
                    //        ConnectionClosed?.DynamicInvoke(task.Exception);
                    //    }
                    //    else
                    //    {
                    //        ConnectionOpened?.DynamicInvoke();
                    //    }
                    //});
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
