
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Hubs;
using DomainPushNotificationApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace DomainPushNotificationApi.Services
{
    public class StartupBackgroundService : BackgroundService
    {
        private readonly DomainNotificationClientService _service;
        private readonly PushNotificationService notificationService;
        private readonly ITokenStore tokenStore;
        private readonly HttpClient http;

        public StartupBackgroundService(DomainNotificationClientService service, PushNotificationService notificationService, ITokenStore tokenStore, IHttpClientFactory factory)
        {
            _service = service;
            this.notificationService = notificationService;
            this.tokenStore = tokenStore;
            this.http = factory.CreateClient("DomainClient");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested && tokenStore.Token.IsNullOrEmpty())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Authentication/gettoken");
                var httpResponse = await http.SendAsync(request);
                var result = await httpResponse.Content.ReadFromJsonAsync<TokenDTO>();
                //if (tokenStore.Token.IsNullOrEmpty() != null)
                //{
                    tokenStore.Token = result.RefreshToken;
                //}
                _service.OnDomainEventsReceived += _service_OnDomainEventsReceived;
                _service.ConnectionClosed += _service_ConnectionClosed;
                await _service.Connect(tokenStore.Token);
            }
            if (stoppingToken.IsCancellationRequested)
            {
                _service.OnDomainEventsReceived -= _service_OnDomainEventsReceived;
                _service.ConnectionClosed -= _service_ConnectionClosed;
                await _service.Disconnect();
            }
        }

        private async void _service_ConnectionClosed(Exception obj)
        {
            await _service.Connect(tokenStore.Token); //why?
        }

        private async void _service_OnDomainEventsReceived(List<DomainEvent>  domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await notificationService.NotifyClients(domainEvent);
            }
        }
    }
}
