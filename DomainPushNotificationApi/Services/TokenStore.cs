namespace DomainPushNotificationApi.Services
{
    public interface ITokenStore
    {
        string Token { get; set; }
    }

    public class TokenStore : ITokenStore
    {
        public string Token { get; set; } //generated on startup once and used to make calls as server to domain
    }
}
