using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace EchoWebapp.Client.Provider
{
    public class CustomAuthStateProvider/*(PersistentComponentState persistentState)*/ : AuthenticationStateProvider
    {
        //private static readonly Task<AuthenticationState> _unauthenticatedTask =
        //Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        //public override Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    if (!persistentState.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
        //    {
        //        return _unauthenticatedTask;
        //    }


        //    //var claims = ParseClaimsFromJwt(await _localStorage.GetItemAsStringAsync("Token"));

        //    Claim[] claims = [
        //        new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserId)];

        //    return Task.FromResult(
        //        new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims,
        //            authenticationType: nameof(CustomAuthStateProvider)))));
        //}

        #region old code
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var token = await _localStorage.GetItemAsStringAsync("Token");
            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var parsedJwt = tokenHandler.ReadJwtToken(token);
                //identity = new ClaimsIdentity(parsedJwt.Claims);
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                _http.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void AuthenticateUser(string userIdentifier)
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, userIdentifier),
        }, "Custom Authentication");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(user)));
        }
        #endregion
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
    public class UserInfo
    {
        public string UserId { get; set; }
    }
}
