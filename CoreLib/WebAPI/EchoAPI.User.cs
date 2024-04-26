using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Models;
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

        public async Task<User> LoginAsync(UserLoginModel log)
        {
            //User user = new()
            //{
            //    Name = name
            //};
            var load = JsonSerializer.Serialize(log, SerializerOptions);
            HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("user/login", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message); 
            }

            return null;
        }

        public async Task<User> CreateUserAsync(RegisterUserModel user)
        {
            //User user = new()
            //{
            //    Name = name
            //};
            var load = JsonSerializer.Serialize(user, SerializerOptions);
            HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("user/createuser", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return null;
        }


        public async Task<User> GetUserWithIncludeAsync(ulong Id)
        {
            var response = await client.GetAsync($"user/a/{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                User result = JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                return result;
            }
            return new User();
        }

    }
}
