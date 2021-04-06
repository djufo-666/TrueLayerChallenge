using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueLayerChallenge.Entities;
using TrueLayerChallenge.ExternalServices;

namespace TrueLayerChallenge.Services
{
    public class PokemonApiService : IPokemonApiService
    {
        public const string URI = "https://funtranslations.com/api/";
        private readonly ILogger<PokemonApiService> _logger;
        private readonly IFunTranslationsService _funTranslationsService;
        private readonly IPokeApiService _pokeApiService;
        private readonly ITranslationStrategy _translationStrategy;

        public PokemonApiService(ILogger<PokemonApiService> logger, IFunTranslationsService funTranslationsService, IPokeApiService pokeApiService, ITranslationStrategy translationStrategy)
        {
            _logger = logger;
            _funTranslationsService = funTranslationsService;
            _pokeApiService = pokeApiService;
            _translationStrategy = translationStrategy;
        }

        public async Task<PokemonInfo> GetByNameAsync(string name)
        {
            try
            {
                var response = await _pokeApiService.GetByNameAsync(name);
                return response != null
                    ? new PokemonInfo
                    {
                        Name = response.Name,
                        Description = response.FlavorTextEntries?
                            .Where(x => x.Language != null)
                            .Where(x => x.Language.Name == "en")
                            .Select(x => x.FlavorText)
                            .FirstOrDefault(),
                        Habitat = response.Habitat?.Name,
                        IsLegendary = response.IsLegendary,
                    }
                    : PokemonInfo.Default;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Pokemon retrieval failed: {name}", ex);
                return PokemonInfo.Default;
            }
        }

        public async Task<PokemonInfo> GetByNameTranslatedAsync(string name)
        {
            PokemonInfo pokemonInfo = await GetByNameAsync(name);

            TranslationType translationType = _translationStrategy.ResolveTranslationType(pokemonInfo);

            string translation = null;
            if (!string.IsNullOrWhiteSpace(pokemonInfo.Description)
                && !string.IsNullOrWhiteSpace(translation = await _funTranslationsService.TranslateAsync(pokemonInfo.Description, translationType))
            )
            {
                pokemonInfo.Description = translation;
            }

            return pokemonInfo;
        }
    }
}
