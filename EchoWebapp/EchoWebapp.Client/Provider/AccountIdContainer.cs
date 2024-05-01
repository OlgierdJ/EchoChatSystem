using System.IdentityModel.Tokens.Jwt;

namespace EchoWebapp.Client.Provider
{
    public class AccountIdContainer
    {
        public ulong Value { get; set; }
        public event Action OnStateChange;
        public void SetValue(string Token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedJwt = tokenHandler.ReadJwtToken(Token);
            this.Value =  (ulong)Convert.ToDecimal(parsedJwt.Subject);
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
