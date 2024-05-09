
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EchoWebapp.Client.Provider
{
    public class CustomAuthStuteProvider : AuthenticationStateProvider
    {
        public string Token { get; set; }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedJwt = tokenHandler.ReadJwtToken(Token);
            var identity = new ClaimsIdentity(parsedJwt.Claims);

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
