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
    public class PokemonApiServiceTests_GetByNameTests: PokemonApiServiceTests_Basic
    {
        [TestMethod]
        async public Task Searching_ByTheName_TranslationsAreNotUsed()
        {
            // Arrange
            SetResponsePokemon(defaultPokemonSpecies);

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameAsync(defaultPokemonInfo.Name);

            // Assert
            pokeApiServiceMoq.VerifyAll();
            translationStrategyMoq.VerifyAll();
            funTranslationsServiceMoq.VerifyAll();
        }

        [TestMethod]
        [DataRow("name1", "desc1", true, "habitat1")]
        [DataRow("name2", "desc2", false, "habitat2")]
        async public Task Given_Spieces_WillMap_Name_EnDescription_IsLegendary_Habitat(
            string name, string description, bool isLegendary, string habitat)
        {
            // Arange
            defaultPokemonSpecies.Name = name;
            defaultPokemonSpecies
                .FlavorTextEntries
                .Single(x => x.Language.Name == "en")
                .FlavorText = description;
            defaultPokemonSpecies.IsLegendary = isLegendary;
            defaultPokemonSpecies.Habitat.Name = habitat;

            SetResponsePokemon(defaultPokemonSpecies);

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameAsync(name);

            // Assert
            Assert.AreEqual(name, pokemonInfo.Name);
            Assert.AreEqual(description, pokemonInfo.Description);
            Assert.AreEqual(isLegendary, pokemonInfo.IsLegendary);
            Assert.AreEqual(habitat, pokemonInfo.Habitat);
        }

        [TestMethod]
        async public Task Given_Spieces_WithNoEnDescription_DescriptionWillBeNull()
        {
            // Arange
            defaultPokemonSpecies.FlavorTextEntries = defaultPokemonSpecies.FlavorTextEntries.Where(x => x.Language.Name != "en").ToArray();

            SetResponsePokemon(defaultPokemonSpecies);

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameAsync(defaultPokemonSpecies.Name);

            // Assert
            Assert.AreEqual(null, pokemonInfo.Description);
        }

        [TestMethod]
        async public Task Given_Spieces_NotFound_DefaultIsReturned()
        {
            // Arange
            pokeApiServiceMoq
                .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<PokemonSpecies>(null));

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameAsync("anyName");

            // Assert
            Assert.AreSame(PokemonInfo.Default, pokemonInfo);
        }

        [TestMethod]
        async public Task ServiceThrowsException_DefaultIsReturned()
        {
            // Arange
            pokeApiServiceMoq
                .Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .Throws(new Exception());

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameAsync("anyName");

            // Assert
            Assert.AreSame(PokemonInfo.Default, pokemonInfo);
        }
    }

}
