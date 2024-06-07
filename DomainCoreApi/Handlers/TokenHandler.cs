using CoreLib.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainCoreApi.Handlers
{
    public class JWTOptions
    {
        public const string Position = "JwtSettings";

        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
        public int DefaultRefreshTokenLifeTimeDays { get; set; } = 0;
        public int DefaultAccessTokenLifeTimeHours { get; set; } = 0;
        public string Key { get; set; } = String.Empty;
        /*
         * "Issuer": "Https://localhost:7269/api",
            "Audience": "https://localhost:7283/",
            "DefaultRefreshTokenLifeTimeDays": 14, //14 days aka 336 hours
            "DefaultAccessTokenLifeTimeHours": 1, //1 hour
            "Key": "WeLoveBigBeautifulWomanAndWeCantLie"//You should not save the key here it should be save at a server that's can handle toke key
         */
    }

    public class TokenHandler
    {
        //https://www.youtube.com/watch?v=mgeuh8k3I4g&t
        private const string TokenKey = "WeLoveBigBeautifulWomanAndWeCantLie";
       
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
        private readonly JWTOptions options;

        public TokenHandler(IOptions<JWTOptions> options) 
        {
            this.options = options.Value;
        }

        public string CreateToken(IEnumerable<Claim> claims, DateTime expires, string issuer, string audience, string secretKey)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
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
            var token = CreateToken(claims, tokenLifeTime, options.Issuer, options.Audience, options.Key);
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

        public string GetAccessToken<T>(T obj) where T : IEntity
        {
            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub,obj.Id.ToString()), //doesnt matter if we leave it for now
                    new(ClaimTypes.NameIdentifier,obj.Id.ToString()),
                };
            var tokenLifeTime = DateTime.UtcNow.Add(TimeSpan.FromHours(options.DefaultAccessTokenLifeTimeHours));
            var token = CreateToken(claims, tokenLifeTime, options.Issuer, options.Audience, options.Key);
            return token;
        }



    }
}
