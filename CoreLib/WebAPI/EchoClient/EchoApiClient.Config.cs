using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoreLib.WebAPI.EchoClient;

public partial class EchoAPIClient
{
    private readonly HttpClient client;
    // private readonly MessageStore _notificationPipeline;

    public JsonSerializerOptions SerializerOptions { get; set; } = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.Preserve

    };

    public EchoAPIClient(HttpClient httpClient)
    {
        //client.BaseAddress = new Uri("http://10.233.42.99/api/");
        //client.BaseAddress = new Uri("https://localhost:7269/api/");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // _notificationPipeline = NotificationPipeline;
        httpClient.BaseAddress = new Uri(httpClient.BaseAddress, "api/");
        client = httpClient;
    }

    private AuthenticationHeaderValue authenticationHeaderValue(string token)
    {
        return new AuthenticationHeaderValue("Bearer", token);
    }
}
