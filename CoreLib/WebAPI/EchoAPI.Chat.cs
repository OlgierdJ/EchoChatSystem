using CoreLib.DTO.RequestCore.ChatCore;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreLib.WebAPI
{
    public partial class EchoAPI
    {
        public async Task<bool> AddChatParticipant(string Token, ulong chatId, ulong participantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/add/participant/{participantId}");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> AddChatParticipants(string Token, ulong chatId, ICollection<ulong> participantIds)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Chat/{chatId}/participant/addmany");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(participantIds, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false; ;
        }

        public async Task<bool> ConsumeChatInvite(string Token, ulong chatId, string inviteCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/invite/use");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(inviteCode, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> CreateChat(string Token, ICollection<ulong> startParticipants)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Chat/create");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(startParticipants, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> CreateChatInvite(string Token, ulong chatId, CreateInviteRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Chat/{chatId}/create/invite");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> DeleteChat(string Token, ulong chatId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Chat/{chatId}");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> HideChat(string Token, ulong chatId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/hide");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> LeaveChat(string Token, ulong chatId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Chat/{chatId}/leave");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> MarkChatAsRead(string Token, ulong chatId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/markRead");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> MuteChat(string Token, ulong chatId, MuteRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/mute");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> PinChatMessage(string Token, ulong chatId, ulong messageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/{messageId}/pin");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> RemoveChatMessage(string Token, ulong chatId, ulong messageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Chat/{chatId}/{messageId}");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> RemoveChatParticipant(string Token, ulong chatId, ulong participantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Chat/{chatId}/remove/participant/{participantId}");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> SendChatMessage(string Token, ulong chatId, SendMessageRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Chat/{chatId}/message");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); ;
            }
            return false;
        }

        public async Task<bool> SetChatImage(string Token, ulong chatId, SetImageRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Chat/{chatId}/message");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        }

        public async Task<bool> UnmuteChat(string Token, ulong chatId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Chat/{chatId}/unmute");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        }

        public async Task<bool> UnpinChatMessage(string Token, ulong chatId, ulong messageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Chat/{chatId}/{messageId}/unpin");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        }

        public async Task<bool> UpdateChat(string Token, ulong chatId, UpdateChatRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/name");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        }

        public async Task<bool> UpdateChatMessage(string Token, ulong chatId, ulong messageId, EditMessageRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Chat/{chatId}/{messageId}");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        }
    }
}
