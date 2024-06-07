using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Services;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CoreLib.WebAPI
{
    public partial class EchoAPI
    {
        public async Task<int> GetDeletedUserCountAsync()
        {
            var response = await client.GetAsync("user/count/deleted").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                int result = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                return result;
            }
            return 0;
        }

        public async Task<int> GetActiveUserCountAsync()
        {
            var response = await client.GetAsync("user/count/active").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                int result = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                return result;
            }
            return 0;
        }

        public async Task<string> LoginAsync(LoginRequestDTO log)
        {
            var load = JsonSerializer.Serialize(log, SerializerOptions);
            HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("user/login", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<TokenDTO>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                    return data.Token;
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return null;
        }

        //public async Task<string> CreateUserAsync(RegisterRequestDTO user)
        //{
        //    var load = JsonSerializer.Serialize(user, SerializerOptions);
        //    HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
        //    try
        //    {
        //        var response = await client.PostAsync("user/createuser", content).ConfigureAwait(false);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
        //        await Console.Out.WriteLineAsync(e.Message);
        //    }

        //    return null;
        //}
        
        //public async Task<UserFullDTO> GetFullUserAsync(string Token)
        //{
        //    try
        //    {
        //        var request = new HttpRequestMessage(HttpMethod.Put, $"user/session");
        //        request.Headers.Authorization = authenticationHeaderValue(Token);
        //        var response = await client.SendAsync(request).ConfigureAwait(false);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return JsonSerializer.Deserialize<UserFullDTO>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
        //        await Console.Out.WriteLineAsync(e.Message);
        //    }

        //    return null;
        //}

        //public async Task<User> GetUserWithIncludeAsync(string Token)
        //{
        //    var response = await client.GetAsync($"user/a/{Id}").ConfigureAwait(false);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        User result = JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        //        return result;
        //    }
        //    return new User();
        //}

        public async Task<bool> UpdatePasswordAsync(string Token, UpdatePasswordRequestDTO u)
        {

            var request = new HttpRequestMessage(HttpMethod.Put, $"user/session");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(u, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.SendAsync(request).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return false;
        }

        public async Task<bool> ForgotPasswordAsync(string Token, PromptResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/ForgotPassword");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(resetPasswordRequestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.SendAsync(request).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return false;
        }

        public async Task<bool> AcceptFriendRequestAsync(string Token,ulong incomingrequestId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/friend/request/{incomingrequestId}/accept");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); 
            }
            return false;
        }

        public async Task<bool> SendFriendRequestAsync(string Token, AddFriendRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"user/friend/request/send");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); 
            }
            return false;
        }

        public async Task<bool> SendFriendRequestAsync(string Token, ulong receiverId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"user/friend/request/send/{receiverId}");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); 
            }
            return false;
        }

        public async Task<bool> CancelFriendRequestAsync(string Token, ulong outgoingrequestId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"user/friend/request/{outgoingrequestId}/cancel");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions); 
            }
            return false;
        }

        public async Task<bool> DeclineFriendRequestAsync(string Token, ulong incomingrequestId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"user/friend/request/{incomingrequestId}/decline");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> RemoveFriendAsync(string Token, ulong friendId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"user/friend/{friendId}/remove");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> BlockUserAsync(string Token, ulong userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/{userId}/block");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> MuteUserAsync(string Token, ulong userId, MuteRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/{userId}/nickname");
            request.Headers.Authorization = authenticationHeaderValue(Token);


            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> SetNicknameAsync(string Token, ulong userId, SetNicknameUserRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/{userId}/mute");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> SetNoteAsync(string Token, ulong userId, SetNoteUserRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/{userId}/note");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> SetUserVolumeAsync(string Token, ulong userId, SetUserVolumeRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/{userId}/changeVolume");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> AddUserConnectionAsync(string Token, AddUserConnectionRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"user/connection/add");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> DeafenSelfAsync(string Token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/deafen");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> DeleteAccountAsync(string Token, DeleteAccountRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"user/account/delete");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> DisableAccountAsync(string Token, DisableAccountRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/account/disable");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> SetPhoneNumberAsync(string Token, EditPhoneNumberRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/phonenumber/change");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<UserFullDTO> LoadUserSessionDataAsync(string Token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"user/session");
                request.Headers.Authorization = authenticationHeaderValue(Token);
                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<UserFullDTO>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return null;
        }

        public async Task<bool> MuteSelfAsync(string Token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/mute");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> RegisterAsync(RegisterRequestDTO requestDTO)
        {
            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("user/register", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return false;
        }

        public async Task<bool> RemoveUserConnectionAsync(string Token, ulong connectionId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"user/connection/{connectionId}");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> SetCustomStatusAsync(string Token, SetCustomStatusRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/status/custom/set");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<bool> SetStatusAsync(string Token, SetStatusRequestDTO requestDTO)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"user/status/set");
            request.Headers.Authorization = authenticationHeaderValue(Token);

            var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
            request.Content = new StringContent(load, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), SerializerOptions);
            }
            return false;
        }

        public async Task<List<ActivityStatusDTO>> GetListOfStatusAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"user/status/getall");
                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<List<ActivityStatusDTO>> (await response.Content.ReadAsStringAsync(), SerializerOptions);
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return null;
        }

        public async Task<bool> SetDisplayNameAsync(string Token, UserMinimalDTO user)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"user/displayname/set");
                request.Headers.Authorization = authenticationHeaderValue(Token);

                var load = JsonSerializer.Serialize(user, SerializerOptions);
                request.Content = new StringContent(load, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return false;
        }
        
        public async Task<bool> StartDirectMessagesAsync(string Token, ulong userId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"user/{userId}/StartDirectMessages");
                request.Headers.Authorization = authenticationHeaderValue(Token);

                //var load = JsonSerializer.Serialize(requestDTO, SerializerOptions);
                //request.Content = new StringContent(load, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
                throw;
            }
        }
    }
}
