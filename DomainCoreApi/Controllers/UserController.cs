using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Handlers;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainCoreApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseEntityController<User, ulong>
    {
        private const string TokenKey = "WeLoveBigBeautifulWomanAndWeCantLie";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(16);
        private readonly IUserService _userService;

        public UserController(IUserService service, IPushNotificationService notificationService) : base(service, notificationService)
        {
            _userService = service;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogins login)
        {
            try
            {
                
                var result = await _userService.LoginUserAsync(login);
                if (result == null)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                //await _notificationService.NotifyClients(result, EntityAction.Create);
                var tokenhandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(TokenKey);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub,result.Id.ToString()),
                    new(JwtRegisteredClaimNames.Email,result.Email),
                    new("UserId",result.Id.ToString())
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
                return Ok(tokenhandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
