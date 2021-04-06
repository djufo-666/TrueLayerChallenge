using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerChallenge.Services;

namespace TrueLayerChallenge.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController: ControllerBase
    {
        private readonly IPokemonApiService _pokemonApiService;
        public PokemonController(IPokemonApiService pokemonApiService)
        {
            _pokemonApiService = pokemonApiService;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ViewModels.Pokemon> GetByNameAsync([FromRoute] string name)
        {
            var pokemon = await _pokemonApiService.GetByNameAsync(name);

            return Map(pokemon);
        }
       
        [HttpGet]
        [Route("translated/{name}")]
        public async Task<ViewModels.Pokemon> GetTranslatedAsync([FromRoute] string name)
        {
            var pokemon = await _pokemonApiService.GetByNameTranslatedAsync(name);

            return Map(pokemon);
        }

        private ViewModels.Pokemon Map(Entities.PokemonInfo pokemon)
        {
            return new ViewModels.Pokemon
            {
                Name = pokemon.Name,
                Description = pokemon.Description,
                Habitat = pokemon.Habitat,
                IsLegendary = pokemon.IsLegendary,
            };
        }
    }
}
