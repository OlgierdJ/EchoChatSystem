using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Handlers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainCoreApi.Handlers;

public class TokenHandler : ITokenHandler
{
    private readonly JWTOptions options;

    public TokenHandler(IOptions<JWTOptions> options)
    {
        this.options = options.Value;
    }

    public string CreateToken(IEnumerable<Claim> claims, DateTime expires, string issuer, string Audience, string secretKey)
    {
        var tokenhandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = issuer,
            Audience = Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenhandler.CreateToken(tokenDescriptor);
        return tokenhandler.WriteToken(token);
    }

    public string GetRefreshToken<T>(T obj) where T : IEntity
    {
        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub,obj.Id.ToString()),
                //new(ClaimTypes.NameIdentifier,obj.Id.ToString()), //nameid is used for access, whereas sub is used for refresh.
            };
        var tokenLifeTime = DateTime.UtcNow.Add(TimeSpan.FromHours(options.DefaultRefreshTokenLifeTimeDays * 24));
        var token = CreateToken(claims, tokenLifeTime, options.Issuer, options.Audiences[0], options.Key);
        //var tokenDescriptor = new SecurityTokenDescriptor
        //{
        //    Subject = new ClaimsIdentity(claims),
        //    Expires = DateTime.UtcNow.Add(TokenLifetime),
        //    Issuer = "Https://localhost:7269/api",
        //    Audience = "https://localhost:7283/",
        //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //};
        return token;
    }

    // Creat a Account Token for the user 
    public string GetAccessToken<T>(T obj) where T : IEntity
    {
        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub,obj.Id.ToString()), //doesnt matter if we leave it for now
                new(ClaimTypes.NameIdentifier,obj.Id.ToString()),
            };
        var tokenLifeTime = DateTime.UtcNow.Add(TimeSpan.FromHours(options.DefaultAccessTokenLifeTimeHours));
        var token = CreateToken(claims, tokenLifeTime, options.Issuer, options.Audiences[0], options.Key);
        return token;
    }
}
