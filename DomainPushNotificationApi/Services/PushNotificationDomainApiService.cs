using CoreLib.DTO.EchoCore.UserCore;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace DomainPushNotificationApi.Services
{

    public class PushNotificationDomainApiService
    {
        private HttpClient _http;

        public PushNotificationDomainApiService(IHttpClientFactory factory, ITokenStore tokenStore) 
        {
            this._http = factory.CreateClient("DomainClient");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenStore.Token);
        }

        public async Task<IEnumerable<string>> GetGroupsByAccountId(ulong accountId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/domain/getgroups/{accountId}");
                var httpResponse = await _http.SendAsync(request);
                var result = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<string>>(new System.Text.Json.JsonSerializerOptions()
                {
                     ReferenceHandler = ReferenceHandler.Preserve
                });
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
