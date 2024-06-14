using Echo.Application.Contracts.Interfaces.Contracts;
using System.Security.Claims;

namespace Echo.Domain.Shared.Handlers;
public interface ITokenHandler
{
    string CreateToken(IEnumerable<Claim> claims, DateTime expires, string issuer, string Audience, string secretKey);
    string GetAccessToken<T>(T obj) where T : IEntity;
    string GetRefreshToken<T>(T obj) where T : IEntity;
}