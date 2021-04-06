using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueLayerChallenge.ExternalServices
{
    public class PokeApiService : IPokeApiService
    {
        public const string URI = "https://pokeapi.co/api/v2/pokemon-species/";
        private readonly ILogger<PokeApiService> _logger;
        private readonly IHttpClientService _client;

        public PokeApiService(ILogger<PokeApiService> logger, IHttpClientService client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Entities.PokeApi.PokemonSpecies> GetByNameAsync(string name)
        {
            string url = $"{URI}{System.Web.HttpUtility.HtmlEncode(name)}";

            try
            {
                var response = await _client.GetAsync<Entities.PokeApi.PokemonSpecies>(url);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Pokemon retrieval failed: {name}", ex);
                return null;
            }
        }
    }
}
