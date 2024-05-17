using CoreLib.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainCoreApi.Handlers
{
    public class TokenHandler
    {
        //https://www.youtube.com/watch?v=mgeuh8k3I4g&t
        private const string TokenKey = "WeLoveBigBeautifulWomanAndWeCantLie";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

        public string CreateToken<T>(T obj) where T : IEntity
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenKey);

            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub,obj.Id.ToString())
                };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "Https://localhost:7269/api",
                Audience = "https://localhost:7283/",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }

       

    }
}
