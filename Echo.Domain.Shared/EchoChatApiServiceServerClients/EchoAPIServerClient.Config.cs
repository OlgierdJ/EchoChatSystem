using Echo.Application.Contracts.Interfaces.Providers;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Echo.Domain.Shared.EchoChatApiServiceServerClients;

public partial class EchoChatApiServiceServerClient
{
    private readonly HttpClient _client;
    private readonly ITokenProvider _tokenStore;

    // private readonly MessageStore _notificationPipeline;

    public JsonSerializerOptions SerializerOptions { get; set; } = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.Preserve

    };

    public EchoChatApiServiceServerClient(HttpClient httpClient, ITokenProvider tokenStore)
    {
        //client.BaseAddress = new Uri("http://10.233.42.99/api/");
        //client.BaseAddress = new Uri("https://localhost:7269/api/");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenStore.RefreshToken); //idk just put longlived here,
                                                                                                                                                   //probably wont even need to do this when authserver is made
        // _notificationPipeline = NotificationPipeline;
        httpClient.BaseAddress = new Uri(httpClient.BaseAddress, "api/");
        _client = httpClient;
        this._tokenStore = tokenStore;
    }

    private AuthenticationHeaderValue authenticationHeaderValue()
    {
        //request.Headers.Authorization = authenticationHeaderValue(Token);
        return new AuthenticationHeaderValue("Bearer", _tokenStore.RefreshToken);
    }
}
