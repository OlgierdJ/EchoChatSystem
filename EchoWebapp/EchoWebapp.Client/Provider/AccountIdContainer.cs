using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace EchoWebapp.Client.Provider
{
    public class AccountIdContainer
    {
        public string Token { get; set; }
        public ulong Value { get; set; }
        public event Action OnStateChange;
        public void SetValue(string Token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedJwt = tokenHandler.ReadJwtToken(Token);
            this.Token = Token;
            this.Value = Convert.ToUInt64(parsedJwt.Subject);
            NotifyStateChanged();
        }
        public void SetValue(ulong id,string token)
        {
            this.Value = id;
            NotifyStateChanged();
        }
        public bool checktime(string? token)
        {
            if (token.IsNullOrEmpty())
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedJwt = tokenHandler.ReadJwtToken(token);
            if (parsedJwt.ValidFrom < DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
