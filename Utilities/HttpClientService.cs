﻿using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilities
{
    public interface IHttpClientService
    {
        Task<string> GetAsync(string uri, string token = "");
        Task<string> PostAsync<T>(string uri, T item, string token = "");
        Task<string> PutAsync<T>(string uri, T item, string token = "");
        Task<string> DeleteAsync<T>(string uri, T item, string token = "");
    }

    public class HttpClientService : IHttpClientService
    {
        private static readonly HttpClient Client = new HttpClient();

        public async Task<string> GetAsync(string uri, string token = "")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if (!string.IsNullOrWhiteSpace(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await Client.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync<T>(string uri, T item, string token = "")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await Client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsync<T>(string uri, T item, string token = "")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await Client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync<T>(string uri, T item, string token = "")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await Client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}

