using Echo.Application.Contracts.Interfaces.Providers;

namespace DomainPushNotificationApi.Services;
public class TokenStore : ITokenProvider
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
