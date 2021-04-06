using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrueLayerChallenge.ExternalServices
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string requestUri, Dictionary<string, string> queryParams = null);
    }
}
