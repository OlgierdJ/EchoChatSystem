using CoreLib.DTO.RequestCore.ChatCore;
using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Handlers;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            this._chatService = chatService;
        }
        [Authorize]
        [HttpPut("{chatId}/add/participant/{participantId}")]
        public async Task<IActionResult> AddChatParticipant(ulong chatId, ulong participantId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.AddChatParticipant(id, chatId, participantId);
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
        [HttpDelete("{chatId}/remove/participant/{participantId}")]
        public async Task<IActionResult> RemoveChatParticipant(ulong chatId, ulong participantId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.RemoveChatParticipant(id, chatId, participantId);
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
        [HttpPost("{chatId}/participant/addmany")]
        public async Task<IActionResult> AddChatParticipants(ulong chatId, AddParticipantsRequestDTO requestDTO) //review
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.AddChatParticipants(id, chatId, requestDTO.ParticipantIds);
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
        [HttpPut("{chatId}/invite/use")]
        public async Task<IActionResult> ConsumeChatInvite(ulong chatId, ConsumeInviteRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.ConsumeChatInvite(id, chatId, requestDTO.InviteCode);
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
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat(AddParticipantsRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.CreateChat(id, requestDTO.ParticipantIds);
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
        [HttpPost("{chatId}/create/invite")]
        public async Task<IActionResult> CreateChatInvite(ulong chatId, CreateInviteRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.CreateChatInvite(id, chatId, requestDTO);
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
        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChat(ulong chatId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.DeleteChat(id, chatId);
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
        [HttpPut("{chatId}/hide")]
        public async Task<IActionResult> HideChat(ulong chatId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.HideChat(id, chatId);
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
        [HttpDelete("{chatId}/leave")]
        public async Task<IActionResult> LeaveChat(ulong chatId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.LeaveChat(id, chatId);
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
        [HttpPut("{chatId}/markRead")]
        public async Task<IActionResult> MarkChatAsRead(ulong chatId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.MarkChatAsRead(id, chatId);
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
        [HttpPut("{chatId}/mute")]
        public async Task<IActionResult> MuteChat(ulong chatId, MuteRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.MuteChat(id, chatId, requestDTO);
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
        [HttpPut("{chatId}/{messageId}/pin")]
        public async Task<IActionResult> PinChatMessage(ulong chatId, ulong messageId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.PinChatMessage(id, chatId, messageId);
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
        [HttpDelete("{chatId}/{messageId}")]
        public async Task<IActionResult> RemoveChatMessage(ulong chatId, ulong messageId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.RemoveChatMessage(id, chatId, messageId);
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
        [HttpPost("{chatId}/message")]
        public async Task<IActionResult> SendChatMessage(ulong chatId, SendMessageRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.SendChatMessage(id, chatId, requestDTO);
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
        [HttpPut("{chatId}/image")]
        public async Task<IActionResult> SetChatImage(ulong chatId, SetImageRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.SetChatImage(id, chatId, requestDTO);
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
        [HttpPut("{chatId}/unmute")]
        public async Task<IActionResult> UnmuteChat(ulong chatId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.UnmuteChat(id, chatId);
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
        [HttpPut("{chatId}/{messageId}/unpin")]
        public async Task<IActionResult> UnpinChatMessage(ulong chatId, ulong messageId)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.UnpinChatMessage(id, chatId, messageId);
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
        [HttpPut("{chatId}/name")]
        public async Task<IActionResult> UpdateChat(ulong chatId, UpdateChatRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.UpdateChat(id, chatId, requestDTO);
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
        [HttpPut("{chatId}/{messageId}")]
        public async Task<IActionResult> UpdateChatMessage(ulong chatId, ulong messageId, EditMessageRequestDTO requestDTO)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);

                var result = await _chatService.UpdateChatMessage(id, chatId, messageId, requestDTO);
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

        [HttpPost("{chatId}/dataset/{NumberOfDataSet}")]
        public async Task<IActionResult> SendChatdatasetMessage(ulong chatId, int NumberOfDataSet)
        {
            try
            {
                var id = Convert.ToUInt64(HttpContext.User.Identity.Name);
                PopulateDatahandler populateDatahandler = new PopulateDatahandler();
                if (populateDatahandler is null)
                {
                    return Problem("the object is null");
                }
                var data = populateDatahandler.GetRandomDateForSendMessageRequest(NumberOfDataSet);
                foreach (var item in data)
                {
                    var result = await _chatService.SendChatMessage(id, chatId, item);
                    if (!result)
                    {
                        return Problem("Something went wrong. Contact an Admin / Server representative");
                    }
                }
                return Ok(data);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("/dataset/{NumberOfDataSet}")]
        public async Task<IActionResult> SendrandomMessagetorandom(int NumberOfDataSet)
        {
            try
            {
                PopulateDatahandler populateDatahandler = new PopulateDatahandler();
                if (populateDatahandler is null)
                {
                    return Problem("the object is null");
                }

                var chats = await _chatService.getListOfchat();
                

                foreach (var item in chats)
                {
                    var data = populateDatahandler.GetRandomDateformessages(item.Participants.ToList(), NumberOfDataSet);
                    await _chatService.sendDataToChat(item.Id,data.ToList());
                }
                return Ok(chats);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
