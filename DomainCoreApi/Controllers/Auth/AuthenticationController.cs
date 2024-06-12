using CoreLib.DTO.EchoCore.UserCore;
using DomainCoreApi.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace DomainCoreApi.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{

    private readonly TokenHandler Handler;
    private readonly JWTOptions options;

    public AuthenticationController(TokenHandler handler, IOptions<JWTOptions> options)
    {
        Handler = handler;
        this.options = options.Value;
    }

    [AllowAnonymous]
    [HttpGet("gettokens")]
    public async Task<IActionResult> GetTokens()
    {
        try
        {
            //Note: look at the token lifetime
            var token = Handler.CreateToken(new List<Claim>(), DateTime.UtcNow.AddDays(options.DefaultRefreshTokenLifeTimeDays), options.Issuer, options.Audiences[1], options.Key);

            return Ok(new TokenDTO { RefreshToken = token });
        }
        catch (Exception)
        {

            return Problem();
        }
    }
}
