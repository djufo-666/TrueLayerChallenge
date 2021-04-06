using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrueLayerChallenge.ExternalServices
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public HttpClientService()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = new JsonSnakeCaseKeyNamingPolicy(),
            };

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }

        public async Task<T> GetAsync<T>(string requestUri, Dictionary<string, string> queryParams = null)
        {
            UriBuilder builder = new UriBuilder(requestUri);

            if (queryParams != null)
            {
                var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
                queryParams.ToList().ForEach(kv =>
                {
                    query[kv.Key] = kv.Value;
                });
                builder.Query = query.ToString();
            }

            T json = await _httpClient.GetStringAsync(builder.Uri)
                .ContinueWith(response =>
                {
                    var result = JsonParse<T>(response.Result);
                    return result;
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            return json;
        }

        private T JsonParse<T>(string json)
        {
            T result = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
            return result;
        }
    }
}
