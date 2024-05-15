using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CoreLib.WebAPI
{
    public partial class EchoAPI
    {
        public string GetController<T>() where T : IEntity, new()
        {
            switch (new T())
            {
                case Account:
                    return "account";
                case AccountSettings:
                    return "accountsettings";
                case AccountBlock:
                    return "accountblock";
                case AccountProfile:
                    return "accountprofile";
                //case CurrentSession:
                //    return "CurrentSession";
                //case Icon:
                //    return "icon";
                //case IconTemplate:
                //    return "icontemplate";
                //case Item:
                //    return "item";
                //case ItemComment:
                //    return "ItemComment";
                //case ItemOrderHistory:
                //    return "ItemOrderHistory";
                //case Location:
                //    return "location";
                //case LoginAttempt:
                //    return "LoginAttempt";
                //case Model:
                //    return "model";
                //case ModelDetail:
                //    return "modeldetail";
                //case ModelImage:
                //    return "modelimage";
                //case Order:
                //    return "order";
                //case OrderComment:
                //    return "ordercomment";
                //case OrderDetail:
                //    return "orderdetail";
                //case OrderDetailComment:
                //    return "orderdetailcomment";
                //case OrderStatus:
                //    return "orderstatus";
                case Role:
                    return "Role";
                case SecurityCredentials:
                    return "SecurityCredentials";
                //case SessionHistory:
                //    return "SessionHistory";
                case User:
                    return "user";
                default:
                    throw new ArgumentException("Controller not found");
            }
        }

        public async Task<HttpStatusCode> DeleteAsync<T>(T entity) where T : IEntity, new()
        {
            HttpResponseMessage response = await client.DeleteAsync($"{GetController<T>()}/{entity.Id}");
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> CreateAsync<T>(T entity) where T : IEntity, new()
        {
            var load = JsonSerializer.Serialize(entity);
            HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{GetController<T>()}", content);
            await Task.Delay(0);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateAsync<T>(T entity) where T : IEntity, new()
        {
            var load = JsonSerializer.Serialize(entity, SerializerOptions);
            HttpContent content = new StringContent(load, Encoding.UTF8, "application/json");
            var response = client.PutAsync($"{GetController<T>()}", content).Result;
            await Task.Delay(0);
            return response.StatusCode;
        }


        public async Task<List<T>> GetEntitiesAsync<T>() where T : IEntity, new()
        {
            var response = await client.GetAsync($"{GetController<T>()}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                if (res != "")
                {
                    List<T> t = JsonSerializer.Deserialize<List<T>>(res, SerializerOptions);
                    return t;
                }
            }
            return new List<T>();
        }

        public async Task<List<T>> GetEntitiesByAccountIdAsync<T>(ulong id) where T : IEntity, new()
        {
            var response = await client.GetAsync($"{GetController<T>()}/ByAccountId/{id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                if (res != "")
                {
                    List<T> t = JsonSerializer.Deserialize<List<T>>(res, SerializerOptions);
                    return t;
                }
            }
            return new List<T>();
        }
        public async Task<List<T>> GetEntitiesWithIncludeAsync<T>() where T : IEntity, new()
        {
            var response = await client.GetAsync($"{GetController<T>()}/include").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                if (res != "")
                {
                    List<T> t = JsonSerializer.Deserialize<List<T>>(res, SerializerOptions);
                    return t;
                }
            }
            return new List<T>();
        }

        public async Task<T> GetEntityAsync<T>(object Id) where T : IEntity, new()
        {
            var response = await client.GetAsync($"{GetController<T>()}/{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                T t = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                return t;
            }
            return new T();
        }

        public async Task<T> GetEntityWithIncludeAsync<T>(object Id) where T : IEntity, new()
        {
            var response = await client.GetAsync($"{GetController<T>()}/{Id}/include").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                T t = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                return t;
            }
            return new T();
        }

        public async Task<int> GetCountAsync<T>() where T : IEntity, new()
        {
            var response = await client.GetAsync($"{GetController<T>()}/count").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                int result = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync(), SerializerOptions);
                return result;
            }
            return 0;
        }
    }
}
