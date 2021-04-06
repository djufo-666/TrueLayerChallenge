using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrueLayerChallenge.Entities.PokeApi;

namespace TrueLayerChallenge.ExternalServices
{
    public class PokeApiServiceSimulated: IPokeApiService
    {
        public PokeApiServiceSimulated() { }

        public async Task<PokemonSpecies> GetByNameAsync(string name)
        {
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Samples\ditto.json");
            PokemonSpecies pokemon = JsonSerializer.Deserialize<PokemonSpecies>(System.IO.File.ReadAllText(path), new JsonSerializerOptions { PropertyNamingPolicy = new JsonSnakeCaseKeyNamingPolicy() });

            var value = new ValueTask<PokemonSpecies>(pokemon);

            return await value;
        }
    }
}
