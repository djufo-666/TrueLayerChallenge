using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrueLayerChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TrueLayerChallenge.ExternalServices;
using TrueLayerChallenge.Entities;
using TrueLayerChallenge.Entities.PokeApi;

namespace TrueLayerChallenge.ServicesTests
{
    [TestClass()]
    public class PokemonApiServiceTests_Basic
    {
        protected Mock<ILogger<PokemonApiService>> loggerMoq;
        protected Mock<IFunTranslationsService> funTranslationsServiceMoq;
        protected Mock<IPokeApiService> pokeApiServiceMoq;
        protected Mock<ITranslationStrategy> translationStrategyMoq;
        protected PokemonApiService sut;
        protected PokemonInfo defaultPokemonInfo;
        protected PokemonSpecies defaultPokemonSpecies;

        [TestInitialize]
        public void Initialize()
        {
            loggerMoq = new Mock<ILogger<PokemonApiService>>();
            funTranslationsServiceMoq = new Mock<IFunTranslationsService>();
            pokeApiServiceMoq = new Mock<IPokeApiService>();
            translationStrategyMoq = new Mock<ITranslationStrategy>();

            sut = new PokemonApiService(
                loggerMoq.Object,
                funTranslationsServiceMoq.Object,
                pokeApiServiceMoq.Object,
                translationStrategyMoq.Object);

            string name = "ditto";
            defaultPokemonInfo = new PokemonInfo
            {
                Name = name,
                Description = $"{name} desc",
                Habitat = "other",
                IsLegendary = false,
            };

            defaultPokemonSpecies = new PokemonSpecies
            {
                Id = 1,
                Name = defaultPokemonInfo.Name,
                Habitat = new NamedAPIResource
                {
                    Name = defaultPokemonInfo.Habitat
                },
                IsLegendary = defaultPokemonInfo.IsLegendary,
                FlavorTextEntries = new FlavorTextEntry[]
                {
                        new FlavorTextEntry
                        {
                            FlavorText = defaultPokemonInfo.Description,
                            Language = new NamedAPIResource
                            {
                                Name = "en",
                            }
                        },
                        new FlavorTextEntry
                        {
                            FlavorText = "French description",
                            Language = new NamedAPIResource
                            {
                                Name = "fr",
                            }
                        }
                },

            };
        }

        protected void SetResponsePokemon(PokemonSpecies pokemonSpecies)
        {
            pokeApiServiceMoq
                .Setup(x => x.GetByNameAsync(It.Is<string>(x => x == pokemonSpecies.Name)))
                .Returns(Task.FromResult(pokemonSpecies))
                ;
            ;
        }
    }

}
