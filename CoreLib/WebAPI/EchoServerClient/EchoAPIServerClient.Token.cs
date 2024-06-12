using Bogus;
using CoreLib.DTO.EchoCore.UserCore;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoreLib.WebAPI.EchoServerClient;

public partial class EchoAPIServerClient
{
    public async Task<TokenDTO> GetTokensAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"authentication/gettokens");
            //request.Headers.Authorization = authenticationHeaderValue(Token);
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<TokenDTO>();
                return res;
            }
        }
        catch (Exception e)
        {
            //if server is not up then exception gets here
            //log here or feedback somehow.
        }

        return null;
    }

    public async Task<bool> GetServerHealthStatusAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"health");
            //request.Headers.Authorization = authenticationHeaderValue(Token);
            var response = await _client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            //_notificationPipeline?.SetCurrentMessage(e.Message, Models.Stores.MessageType.Error);
            await Console.Out.WriteLineAsync(e.Message);
        }

        return false;
    }
}
