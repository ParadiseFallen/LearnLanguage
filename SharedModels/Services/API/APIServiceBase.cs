using Models.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Models.Services.API
{
    public abstract class APIServiceBase<T>
    {
        public HttpApiClient Http { get; private set; }
        public JsonSerializerOptions SerializerOptions { get; private set; }
        public APIServiceBase(HttpApiClient httpApiClient, JsonSerializerOptions serializerOptions)
        {
            if (httpApiClient == null)
                throw new NullReferenceException($"HttpApiClient is null");
            SerializerOptions = serializerOptions;
            Http = httpApiClient;
        }

        public virtual async Task<APIResponse<T>> GetAsync(string Uri)
        {
            try
            {
                var response = await Http.Client.GetAsync(Uri);
                return await response.Content.ReadFromJsonAsync<APIResponse<T>>(SerializerOptions);
            }
            catch (Exception ex)
            {
                return new APIResponse<T> {Errors = new[] { "Server not responding" } };
            }
        }

        public virtual async Task<APIResponse<T>> PostAsync(string Uri,T data)
        {
            try
            {
                var response = await Http.Client.PostAsJsonAsync(Uri, data,SerializerOptions);
                return await response.Content.ReadFromJsonAsync<APIResponse<T>>(SerializerOptions);
            }
            catch (Exception ex)
            {
                return new APIResponse<T> { Errors = new[] { "Server not responding" } };
            }
        }
        public virtual async Task<APIResponse<T>> PutAsync(string Uri)
        {
            try
            {
                return await Http.Client.GetFromJsonAsync<APIResponse<T>>(Uri, SerializerOptions);
            }
            catch (Exception ex)
            {
                return new APIResponse<T> { Errors = new[] { "Server not responding" } };
            }
        }
        public virtual async Task<APIResponse<T>> DeleteAsync(string Uri)
        {
            try
            {
                return await Http.Client.GetFromJsonAsync<APIResponse<T>>(Uri, SerializerOptions);
            }
            catch (Exception ex)
            {
                return new APIResponse<T> { Errors = new[] { "Server not responding" } };
            }
        }
        public virtual void Parse()
        { 
            
        }
    }
}
