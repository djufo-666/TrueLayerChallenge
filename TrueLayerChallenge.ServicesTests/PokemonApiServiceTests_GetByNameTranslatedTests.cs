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
    public class PokemonApiService_GetByNameTranslatedTests: PokemonApiServiceTests_Basic
    {
        [TestMethod]
        [DataRow(TranslationType.Yoda)]
        [DataRow(TranslationType.Shakespeare)]
        async public Task TranslationStrategy_IsUsed_ForTranslations(TranslationType translationType)
        {
            // Arrange
            SetResponsePokemon(defaultPokemonSpecies);

            translationStrategyMoq
                .Setup(x => x.ResolveTranslationType(It.IsAny<PokemonInfo>()))
                .Returns(translationType);
            
            funTranslationsServiceMoq
                .Setup(
                    x => x.TranslateAsync(It.IsAny<string>(),
                    It.Is<TranslationType>(x => x == translationType)))
                .Returns(Task.FromResult(translationType.ToString()));

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameTranslatedAsync(defaultPokemonInfo.Name);

            // Assert
            funTranslationsServiceMoq.VerifyAll();
            Assert.AreEqual(translationType.ToString(), pokemonInfo.Description);
        }

        [TestMethod]
        async public Task Given_Description_ItIsTranslated()
        {
            // Arange
            string description = "Description for translation";
            string tranlated = "Translated Description";

            defaultPokemonSpecies
                .FlavorTextEntries
                .Single(x => x.Language.Name == "en")
                .FlavorText = description;

            SetResponsePokemon(defaultPokemonSpecies);

            funTranslationsServiceMoq
                .Setup(x => x.TranslateAsync(
                    It.Is<string>(d => d == description), 
                    It.IsAny<TranslationType>()))
                .Returns(Task.FromResult(tranlated));


            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameTranslatedAsync(defaultPokemonSpecies.Name);

            // Assert
            Assert.AreEqual(tranlated, pokemonInfo.Description);
            funTranslationsServiceMoq.VerifyAll();
        }

        [TestMethod]
        async public Task Given_DescriptionNull_ItIsNotTranslated()
        {
            // Arange
            defaultPokemonSpecies
                .FlavorTextEntries
                .Single(x => x.Language.Name == "en")
                .FlavorText = null;

            SetResponsePokemon(defaultPokemonSpecies);

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameTranslatedAsync(defaultPokemonSpecies.Name);

            // Assert
            funTranslationsServiceMoq.VerifyAll();
        }

        [TestMethod]
        async public Task Given_TranslationIsNull_OrigianlDescriptionReturned()
        {
            // Arange
            string expectedDescription = "expected description";
            defaultPokemonSpecies
                .FlavorTextEntries
                .Single(x => x.Language.Name == "en")
                .FlavorText = expectedDescription;

            SetResponsePokemon(defaultPokemonSpecies);
            funTranslationsServiceMoq
                .Setup(x => x.TranslateAsync(
                    It.IsAny<string>(), 
                    It.IsAny<TranslationType>()))
                .Returns(Task.FromResult<string>(null));

            // Act
            PokemonInfo pokemonInfo = await sut.GetByNameTranslatedAsync(defaultPokemonSpecies.Name);

            // Assert
            funTranslationsServiceMoq.VerifyAll();
            Assert.AreEqual(expectedDescription, pokemonInfo.Description);
        }
    }

}
