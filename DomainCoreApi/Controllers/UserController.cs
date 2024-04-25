using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Handlers;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Services;
using CoreLib.Models;
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
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
