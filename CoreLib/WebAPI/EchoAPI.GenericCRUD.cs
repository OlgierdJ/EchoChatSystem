using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CoreLib.WebAPI
{
    public partial class EchoAPI
    {
        //public string GetController<T>() where T : IEntity, new()
        //{
        //    switch (new T())
        //    {
        //        case Account:
        //            return "account";
        //        case AccountSettings:
        //            return "accountsettings";
        //        case AccountBlock:
        //            return "accountblock";
        //        case AccountProfile:
        //            return "accountprofile";
        //        //case CurrentSession:
        //        //    return "CurrentSession";
        //        //case Icon:
        //        //    return "icon";
        //        //case IconTemplate:
        //        //    return "icontemplate";
        //        //case Item:
        //        //    return "item";
        //        //case ItemComment:
        //        //    return "ItemComment";
        //        //case ItemOrderHistory:
        //        //    return "ItemOrderHistory";
        //        //case Location:
        //        //    return "location";
        //        //case LoginAttempt:
        //        //    return "LoginAttempt";
        //        //case Model:
        //        //    return "model";
        //        //case ModelDetail:
        //        //    return "modeldetail";
        //        //case ModelImage:
        //        //    return "modelimage";
        //        //case Order:
        //        //    return "order";
        //        //case OrderComment:
        //        //    return "ordercomment";
        //        //case OrderDetail:
        //        //    return "orderdetail";
        //        //case OrderDetailComment:
        //        //    return "orderdetailcomment";
        //        case Chat:
        //            return "Chat";
        //        case Role:
        //            return "Role";
        //        case SecurityCredentials:
        //            return "SecurityCredentials";
        //        //case SessionHistory:
        //        //    return "SessionHistory";
        //        case User:
        //            return "user";
        //        default:
        //            throw new ArgumentException("Controller not found");
        //    }
        //}

        //public async Task<HttpStatusCode> DeleteAsync<T>(T entity, AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Delete, $"{GetController<T>()}/{entity.Id}");
        //    request.Headers.Authorization = authorization;

        //    HttpResponseMessage response = await client.SendAsync(request);
        //    return response.StatusCode;
        //}

        //public async Task<HttpStatusCode> CreateAsync<T>(T entity, AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Post, $"{GetController<T>()}");
        //    request.Headers.Authorization = authorization;

        //    var load = JsonSerializer.Serialize(entity);
        //    request.Content = new StringContent(load, Encoding.UTF8, "application/json");
            
        //    var response = await client.SendAsync(request);
        //    await Task.Delay(0);
        //    return response.StatusCode;
        //}

        //public async Task<HttpStatusCode> UpdateAsync<T>(T entity, AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Put, $"{GetController<T>()}");
        //    request.Headers.Authorization = authorization;
            
        //    var load = JsonSerializer.Serialize(entity, SerializerOptions);
        //    request.Content = new StringContent(load, Encoding.UTF8, "application/json");
        //    var response = client.SendAsync(request).Result;
        //    await Task.Delay(0);
        //    return response.StatusCode;
        //}


        //public async Task<List<T>> GetEntitiesAsync<T>(AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"{GetController<T>()}");
        //    request.Headers.Authorization = authorization;

        //    var response = await client.SendAsync(request).ConfigureAwait(false);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var res = await response.Content.ReadAsStringAsync();
        //        if (res != "")
        //        {
        //            List<T> t = JsonSerializer.Deserialize<List<T>>(res, SerializerOptions);
        //            return t;
        //        }
        //    }
        //    return new List<T>();
        //}

        //public async Task<List<T>> GetEntitiesWithIncludeAsync<T>(AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"{GetController<T>()}/include");
        //    request.Headers.Authorization = authorization;

        //    var response = await client.SendAsync(request).ConfigureAwait(false);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var res = await response.Content.ReadAsStringAsync();
        //        if (res != "")
        //        {
        //            List<T> t = JsonSerializer.Deserialize<List<T>>(res, SerializerOptions);
        //            return t;
        //        }
        //    }
        //    return new List<T>();
        //}

        //public async Task<T> GetEntityAsync<T>(object Id, AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"{GetController<T>()}/{Id}");
        //    request.Headers.Authorization = authorization;

        //    //var response = await client.GetAsync($"{GetController<T>()}/{Id}").ConfigureAwait(false);
        //    var response = await client.SendAsync(request).ConfigureAwait(false); 
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var res = await response.Content.ReadAsStringAsync();
        //        T t = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        //        return t;
        //    }
        //    else if (response.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        return new T();
        //    }
        //    return new T();
        //}

        //public async Task<T> GetEntityWithIncludeAsync<T>(object Id, AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"{GetController<T>()}/{Id}/include");
        //    request.Headers.Authorization = authorization;

        //    var response = await client.SendAsync(request).ConfigureAwait(false);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        T t = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        //        return t;
        //    }
        //    return new T();
        //}

        //public async Task<int> GetCountAsync<T>(AuthenticationHeaderValue authorization) where T : IEntity, new()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"{GetController<T>()}/count");
        //    request.Headers.Authorization = authorization;

        //    var response = await client.SendAsync(request).ConfigureAwait(false);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        int result = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync(), SerializerOptions);
        //        return result;
        //    }
        //    return 0;
        //}
    }
}
