using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

namespace Sapiens.Core.Services
{
    public class EntityService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _conf;
        private readonly ILogger<EntityService> _log;
        private readonly string _baseUri;
        private string _name { get; set; }

        public EntityService(HttpClient http, IConfiguration conf, ILogger<EntityService> log)
        {
            _http = http;
            _conf = conf;
            _log = log;
            _baseUri = _conf.GetSection("ServiceUri").Value;
            _name = this.GetType().Name.Replace("Service", "");
        }

        public async Task GetAsync(string endpoint = null)
        {
            var uri = CreateUri(endpoint);
            var response = await _http.GetAsync(uri);
            CheckResponse(response);
        }

        public async Task<string> GetStringAsync(string endpoint = null)
        {
            var uri = CreateUri(endpoint);
            try
            {
                return await _http.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
            }
            return default;
        }

        protected async Task<byte[]> GetByteArrayAsync(string endpoint = null)
        {
            var uri = CreateUri(endpoint);
            try
            {
                return await _http.GetByteArrayAsync(uri);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
            }
            return default;
        }

        protected async Task<T> GetJsonAsync<T>(string endpoint = null)
        {
            try
            {
                var uri = CreateUri(endpoint);
                var response = await _http.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None);
                if (CheckResponse(response) && ValidateJsonContent(response.Content))
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
            }
            catch { }
            return default;
        }

        protected async Task PutAsync(string endpoint = null)
        {
            try
            {
                var uri = CreateUri(endpoint);
                var response = await _http.PutAsync(uri, null);
                CheckResponse(response);
            }
            catch { }
        }
        protected async Task<T> PutJsonAsync<T>(T value, string endpoint = null)
        {
            return await PutJsonAsync<T, T>(value, endpoint);
        }

        protected async Task<TResult> PutJsonAsync<TValue, TResult>(TValue value, string endpoint = null)
        {
            try
            {
                var uri = CreateUri(endpoint);
                var response = await _http.PutAsJsonAsync(uri, value);
                if (CheckResponse(response) && ValidateJsonContent(response.Content))
                {
                    var result = await response.Content.ReadFromJsonAsync<TResult>();
                    return result;
                }
            }
            catch { }
            return default;
        }

        protected async Task PostAsync(string endpoint = null)
        {
            try
            {
                var uri = CreateUri(endpoint);
                var response = await _http.PostAsync(uri, null);
                CheckResponse(response);
            }
            catch { }
        }

        protected async Task<T> PostJsonAsync<T>(T value, string endpoint = null)
        {
            return await PostJsonAsync<T, T>(value, endpoint);
        }

        protected async Task<TResult> PostJsonAsync<TValue, TResult>(TValue value, string endpoint = null)
        {
            try
            {
                var uri = CreateUri(endpoint);
                var response = await _http.PostAsJsonAsync(uri, value);
                if (CheckResponse(response) && ValidateJsonContent(response.Content))
                {
                    var result = await response.Content.ReadFromJsonAsync<TResult>();
                    return result;
                }
            }
            catch { }
            return default;
        }

        protected async Task DeleteAsync(string endpoint = null)
        {
            var uri = CreateUri(endpoint);
            var response = await _http.DeleteAsync(uri);
            CheckResponse(response);
        }

        private bool ValidateJsonContent(HttpContent content)
        {
            var mediaType = content?.Headers.ContentType?.MediaType;
            return mediaType != null && mediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
        }

        private bool CheckResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return true;
            if (response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != HttpStatusCode.NotFound)
            {
                _log.LogError($"Request: {response.RequestMessage.RequestUri}");
                _log.LogError($"Response status: {response.StatusCode} {response.ReasonPhrase}");
            }
            return false;
        }

        private string CreateUri(string endpoint)
        {
            var uri = $"{_baseUri}/{_name}";
            if (string.IsNullOrEmpty(endpoint))
                return uri;
            else return $"{uri}/{endpoint}";
        }
    }
}
