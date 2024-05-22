using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore.UserCore;
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
                    var data = JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                    return data;
                }
            }
            catch (Exception e)
            {
                //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
                await Console.Out.WriteLineAsync(e.Message);
            }

            return null;
        }

        public async Task<string> CreateUserAsync(RegisterRequestDTO user)
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
                    return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync(), SerializerOptions);
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

        public async Task<bool> UpdatePasswordAsync(UpdatePasswordRequestDTO u)
        {

            var load = JsonSerializer.Serialize(u, SerializerOptions);
            HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync("user/updatepassword", content).ConfigureAwait(false);
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

    }
}
