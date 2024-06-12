using System.Net.Http.Json;
namespace CoreLib.WebAPI.EchoServerClient;

public partial class EchoAPIServerClient
{
    public async Task<IEnumerable<string>> GetGroupsByAccountId(ulong accountId)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"domain/getgroups/{accountId}");
            var httpResponse = await _client.SendAsync(request);
            var result = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<string>>(SerializerOptions);
            return result;
        }
        catch (Exception e)
        {

            throw;
        }
    }
}
