using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _Client;

        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration configuration, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;

            _Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["ClientAddress"])
            };
            var headers = _Client.DefaultRequestHeaders.Accept;

            headers.Clear();

            headers.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> GetAsync<T>(string url) where T : new()
        {
            var response = await _Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>();
            return new T();
        }

        protected T Get<T>(string url) where T : new() => GetAsync<T>(url).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var response = await _Client.PostAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
        {
            var response = await _Client.PutAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url) => await _Client.DeleteAsync(url);

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
    }
}
