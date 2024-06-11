using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Services;
//using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainCoreApi.Handlers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CoreLib.DTO.RequestCore.UserCore;
using Microsoft.AspNetCore.Identity.Data;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.Handlers;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase //: BaseEntityController<User, ulong>
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)// : base(service, notificationService)
        {
            _userService = service;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequestDTO login)
        {
            try
            {

                var result = await _userService.LoginAsync(login);
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
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAuthenticationAsync(TokenDTO prevTokens)
        {
            try
            {
                var token = prevTokens.RefreshToken;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var rawId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
                if (rawId==null || rawId == "")
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                var id = Convert.ToUInt64(rawId);
                var result = await _userService.RefreshAuthenticationAsync(id, prevTokens.RefreshToken);
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
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync(TokenDTO prevTokens)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.LogoutAsync(id, prevTokens.RefreshToken);
                if (result == false)
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
        [HttpPut("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync(PromptResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            try
            {
                
                var result = await _userService.ForgotPasswordAsync(resetPasswordRequestDTO.Username, resetPasswordRequestDTO.Username); 
                //essentially login but remotely????
                //note: validate username & email, make temp accesstoken, store token in db, send mail with echo.chat/activateresetpassword&token={tokenhere}
                



                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        //[AllowAnonymous]
        //[HttpPut("ResetPassword")]
        //public async Task<IActionResult> ResetPassword(UpdatePasswordRequestDTO updatePasswordRequestDTO)
        //{
        //    try
        //    {
        //        var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

        //        //note: get token, validate still works, get id from token, change user password



        //        var result = await _userService.ResetPassword(id, updatePasswordRequestDTO);
        //        if (!result)
        //        {
        //            return Problem("Something went wrong. Contact an Admin / Server representative");
        //        }
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        return Problem(ex.Message);
        //    }
        //}

        [Authorize]
        [HttpGet("session")]
        public async Task<IActionResult> LoadUserSessionDataAsync()
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                //var token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
                //var handler = new JwtSecurityTokenHandler();
                //var jwtSecurityToken = handler.ReadJwtToken(token);
                //var id = Convert.ToUInt64(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

                var result = await _userService.LoadUserSessionDataAsync(id);
                if (result==null)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                //var token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
                //var handler = new JwtSecurityTokenHandler();
                //var jwtSecurityToken = handler.ReadJwtToken(token);
                //var id = Convert.ToUInt64(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
               
                
                

                var result = await _userService.UpdatePasswordAsync(id, requestDTO.NewPassword);
                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("friend/request/{incomingrequestId}/accept")]
        public async Task<IActionResult> AcceptFriendRequestAsync(ulong incomingrequestId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.AcceptFriendRequestAsync(id, incomingrequestId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("friend/request/send/{userId}")]
        public async Task<IActionResult> SendFriendRequestAsync(ulong userId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SendFriendRequestAsync(id, userId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("friend/request/send")]
        public async Task<IActionResult> SendFriendRequestAsync(AddFriendRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SendFriendRequestAsync(id, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("friend/request/{outgoingrequestId}/cancel")]
        public async Task<IActionResult> CancelFriendRequestAsync(ulong outgoingrequestId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.CancelFriendRequestAsync(id, outgoingrequestId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("friend/request/{incomingrequestId}/decline")]
        public async Task<IActionResult> DeclineFriendRequestAsync(ulong incomingrequestId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.DeclineFriendRequestAsync(id, incomingrequestId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("friend/{friendId}/remove")]
        public async Task<IActionResult> RemoveFriendAsync(ulong friendId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.RemoveFriendAsync(id, friendId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{userId}/block")]
        public async Task<IActionResult> BlockUserAsync(ulong userId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.BlockUserAsync(id, userId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{userId}/unblock")]
        public async Task<IActionResult> UnblockUserAsync(ulong userId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.UnblockUserAsync(id, userId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{userId}/mute")]
        public async Task<IActionResult> MuteUserAsync(ulong userId, MuteRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.MuteUserAsync(id, userId, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{userId}/unmute")]
        public async Task<IActionResult> UnmuteUserAsync(ulong userId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.UnmuteUserAsync(id, userId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{userId}/nickname")]
        public async Task<IActionResult> SetNicknameAsync(ulong userId, SetNicknameUserRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SetNicknameAsync(id, userId, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{userId}/note")]
        public async Task<IActionResult> SetNoteAsync(ulong userId, SetNoteUserRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SetNoteAsync(id, userId, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{userId}/changeVolume")]
        public async Task<IActionResult> SetUserVolumeAsync(ulong userId, SetUserVolumeRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SetUserVolumeAsync(id, userId, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("connection/add")]
        public async Task<IActionResult> AddUserConnectionAsync(AddUserConnectionRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.AddUserConnectionAsync(id, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("deafen")]
        public async Task<IActionResult> DeafenSelfAsync()
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.DeafenSelfAsync(id);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("undeafen")]
        public async Task<IActionResult> UndeafenSelfAsync()
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.UndeafenSelfAsync(id);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("account/delete")] //does not delete
        public async Task<IActionResult> DeleteAccountAsync(DeleteAccountRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.DeleteAccountAsync(id, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("account/disable")] //does not delete
        public async Task<IActionResult> DisableAccountAsync(DisableAccountRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.DisableAccountAsync(id, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("phonenumber/change")]
        public async Task<IActionResult> SetPhoneNumberAsync(EditPhoneNumberRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SetPhoneNumberAsync(id, requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("mute")]
        public async Task<IActionResult> MuteSelfAsync()
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.MuteSelfAsync(id);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("unmute")]
        public async Task<IActionResult> UnmuteSelfAsync()
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.UnmuteSelfAsync(id);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestDTO requestDTO)
        {
            try
            {
                var result = await _userService.RegisterAsync(requestDTO);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("connection/{connectionId}")]
        public async Task<IActionResult> RemoveUserConnectionAsync(ulong connectionId, UpdateUserConnectionRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.UpdateUserConnectionAsync(id, connectionId, requestDTO);

                if (!result)
                {
                    return NotFound("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("connection/{connectionId}")]
        public async Task<IActionResult> RemoveUserConnectionAsync(ulong connectionId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.RemoveUserConnectionAsync(id, connectionId);

                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("status/custom/set")]
        public async Task<IActionResult> SetCustomStatusAsync(SetCustomStatusRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SetCustomStatusAsync(id, requestDTO);




                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("status/set")]
        public async Task<IActionResult> SetStatusAsync(SetStatusRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SetStatusAsync(id, requestDTO);




                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet("status/getall")]
        public async Task<IActionResult> GetListOfStatusAsync()
        {
            try
            {
                var result = await _userService.GetListOfStatusAsync();
                if (result == null)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("displayname/set")]
        public async Task<IActionResult> SetDisplayNameAsync(UserMinimalDTO user)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.SetDisplayNameAsync(id, user);


                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }


        [Authorize]
        [HttpPut("{userId}/StartDirectMessages")]
        public async Task<IActionResult> StartDirectMessagesAsync(ulong userId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.StartDirectMessages(id, userId);




                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{userId}/StartDirectMessages")]
        public async Task<IActionResult> StartDirectMessagesAsync(ulong userId, SendMessageRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                var result = await _userService.StartDirectMessages(id, userId, requestDTO);




                if (!result)
                {
                    return Problem("Something went wrong. Contact an Admin / Server representative");
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("AddDataSet")]
        public async Task<IActionResult> AddDataSet(int NumberOfDataSet)
        {
            try
            {
                PopulateDatahandler populateDatahandler = new PopulateDatahandler();
                if (populateDatahandler is null)
                {
                    return Problem("the object is null");
                }
                var data = populateDatahandler.GetRandomData(NumberOfDataSet);
                foreach (var item in data)
                {
                    await _userService.RegisterAsync(item);
                }

                return Ok(data);

            }
            catch (Exception e)
            {

                return Problem(e.Message); ;
            }
        }

        [AllowAnonymous]
        [HttpPost("friend/request/sendRandom")]
        public async Task<IActionResult> SendRandomFriendRequestAsync(int NumberOfDataSet)
        {
            try
            {
                PopulateDatahandler populateDatahandler = new PopulateDatahandler();
                if (populateDatahandler is null)
                {
                    return Problem("the object is null");
                }
               
                var result = await _userService.SendRandomFriendRequestAsync();

                var data = populateDatahandler.GetRandomFriendRequest(NumberOfDataSet, result.Select(e => e.Item2).ToList()).ToList();
                var senderid = populateDatahandler.GetRandomUserid(NumberOfDataSet, result.Select(e => e.Item1).ToList()).ToList();
                var id = result.Select(e => e.Item1).ToList();
                var cont = id.Count-1;
                Random r = new Random();
                for (int i = 0; i < NumberOfDataSet; i++)
                {
                    await _userService.SendFriendRequestAsync(id[r.Next(0, cont)], data[r.Next(0, NumberOfDataSet-1)]);
                    await _userService.StartDirectMessages(id[r.Next(0, cont)], senderid[r.Next(0, NumberOfDataSet-1)].userid);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}