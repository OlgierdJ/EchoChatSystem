using Echo.Application.Contracts.Interfaces.Providers;
using Echo.Domain.Shared.DomainEvents;
using Echo.Domain.Shared.EchoChatApiServiceServerClients;
using Microsoft.IdentityModel.Tokens;
namespace DomainPushNotificationApi.Services;

public class StartupBackgroundService : BackgroundService
{
    private readonly DomainNotificationClientService _service;
    private readonly PushNotificationService notificationService;
    private readonly ITokenProvider tokenStore;
    private readonly EchoChatApiServiceServerClient _echoAPIServerClient;

    public StartupBackgroundService(DomainNotificationClientService service, PushNotificationService notificationService, ITokenProvider tokenStore, EchoChatApiServiceServerClient echoAPIServerClient)
    {
        _service = service;
        this.notificationService = notificationService;
        this.tokenStore = tokenStore;
        this._echoAPIServerClient = echoAPIServerClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested && tokenStore.RefreshToken.IsNullOrEmpty())
        {
            while (tokenStore.RefreshToken.IsNullOrEmpty())
            {
                var result = await _echoAPIServerClient.GetTokensAsync();
                tokenStore.RefreshToken = result?.RefreshToken;
                if (tokenStore.RefreshToken is null)
                {
                    //wait for 3 seconds before retrying.  
                    await Task.Delay(3000);
                };
            }
            //if (tokenStore.Token.IsNullOrEmpty() != null)
            //{
            //}
            _service.OnDomainEventsReceived += _service_OnDomainEventsReceived;
            _service.ConnectionClosed += _service_ConnectionClosed;
            await _service.ConnectAsync(tokenStore.RefreshToken);
        }
        if (stoppingToken.IsCancellationRequested)
        {
            _service.OnDomainEventsReceived -= _service_OnDomainEventsReceived;
            _service.ConnectionClosed -= _service_ConnectionClosed;
            await _service.DisconnectAsync();
        }
    }

    private async void _service_ConnectionClosed(Exception obj)
    {
        await _service.ConnectAsync(tokenStore.RefreshToken); //why?
    }

    private async void _service_OnDomainEventsReceived(List<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await notificationService.NotifyClients(domainEvent);
        }
    }
}
