using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace CoreLib.WebAPI
{
    public partial class EchoAPI
    {
        private readonly HttpClient client = new();
        // private readonly MessageStore _notificationPipeline;

        public JsonSerializerOptions SerializerOptions { get; set; } = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
            
        };

        public EchoAPI(/*MessageStore NotificationPipeline = null*/)
        {
            //client.BaseAddress = new Uri("http://10.233.42.99/api/");
            client.BaseAddress = new Uri("https://localhost:7269/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // _notificationPipeline = NotificationPipeline;
        }
    }
}
