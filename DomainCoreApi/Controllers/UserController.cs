using CoreLib.DTO.EchoCore.RequestCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainCoreApi.Handlers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DomainCoreApi.Controllers
{
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
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync(RegisterRequestDTO input)
        {
            try
            {
                var result = await _userService.CreateUserAsync(input);
                if (result is null)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok("all looks good");
            }
            catch (Exception ex)
            {

                return Problem(ex.Message); ;
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO login)
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

        [AllowAnonymous]
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordRequestDTO u)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var id = Convert.ToUInt64(jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);
               
                
                

                var result = await _userService.UpdatePassword(id, u.NewPassword);
                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok("all looks good");
            }
            catch (Exception ex)
            {

                return Problem(ex.Message); ;
            }
        }
    }
}