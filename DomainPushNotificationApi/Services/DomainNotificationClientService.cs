using CoreLib;
using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.Hubs;
using Microsoft.AspNetCore.SignalR.Client;

namespace DomainPushNotificationApi.Services
{
    public class DomainNotificationClientService : IDisposable
    {
        private string ServerIP = "https://localhost:7269/DomainPushNotificationHub"; //example "https://localhost:7269/DomainPushNotificationHub"
        private readonly PushNotificationService pushNotificationService;

        public HubConnection Connection { get; set; }

        public Action Disposing { get; set; }

        #region list of Action the hub can do
        public event Action<Exception> ConnectionClosed;
        public event Action<Exception> OnConnectedAsync;
        public event Action<List<DomainEvent>> OnDomainEventsReceived;
        #endregion

        public DomainNotificationClientService(PushNotificationService pushNotificationService)
        {
            Disposing = () =>
            {
                Connection?.DisposeAsync();
            };
            this.pushNotificationService = pushNotificationService;
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

            connection.On<List<DomainEvent>>(nameof(IDomainNotificationHub.ReceiveDomainEvents), (events) => OnDomainEventsReceived?.Invoke(events));
            connection.ServerTimeout = TimeSpan.FromSeconds(2);
            

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

        public void Dispose()
        {
            Disposing();
        }
    }
}
