
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
        private readonly HttpClient http;

        private string Token { get; set; }

        public StartupBackgroundService(DomainNotificationClientService service, PushNotificationService notificationService, IHttpClientFactory factory)
        {
            _service = service;
            this.notificationService = notificationService;
            this.http = factory.CreateClient("DomainClient");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested && Token.IsNullOrEmpty())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Authentication/gettoken");
                var httpResponse = await http.SendAsync(request);
                var result = await httpResponse.Content.ReadFromJsonAsync<TokenDTO>();
                Token = result.RefreshToken;
                _service.OnDomainEventsReceived += _service_OnDomainEventsReceived;
                _service.ConnectionClosed += _service_ConnectionClosed;
                await _service.Connect(Token);
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
            await _service.Connect(Token);
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
