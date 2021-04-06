using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueLayerChallenge.Entities;
using TrueLayerChallenge.Services;

namespace TrueLayerChallenge.ServicesTests
{
    [TestClass()]
    public class TranslationStrategyTests
    {
        TranslationStrategy translationStrategy;

        [TestInitialize]
        public void Initialize()
        {
            translationStrategy = new TranslationStrategy();
        }

        [TestMethod()]
        public void GivenPokemon_Habitat_Cave_ShouldReturn_Yoda()
        {
            // Arrange
            PokemonInfo pokemonInfo = new PokemonInfo
            {
                Habitat = "cave",
                IsLegendary = false,
            };

            // Act
            TranslationType translationType = translationStrategy.ResolveTranslationType(pokemonInfo);

            // Assert 
            Assert.AreEqual(TranslationType.Yoda, translationType);
        }

        [TestMethod()]
        public void GivenPokemon_Habitat_Other_ShouldReturn_Shakespeare()
        {
            // Arrange
            PokemonInfo pokemonInfo = new PokemonInfo
            {
                Habitat = "other",
                IsLegendary = false,
            };

            // Act
            TranslationType translationType = translationStrategy.ResolveTranslationType(pokemonInfo);

            // Assert 
            Assert.AreEqual(TranslationType.Shakespeare, translationType);
        }


        [TestMethod()]
        public void GivenPokemon_IsLegendary_true_ShouldReturn_Yoda()
        {
            // Arrange
            PokemonInfo pokemonInfo = new PokemonInfo
            {
                Habitat = "other",
                IsLegendary = true,
            };

            // Act
            TranslationType translationType = translationStrategy.ResolveTranslationType(pokemonInfo);

            // Assert 
            Assert.AreEqual(TranslationType.Yoda, translationType);
        }

        [TestMethod()]
        public void GivenPokemon_IsLegendary_false_ShouldReturn_Shakespeare()
        {
            // Arrange
            PokemonInfo pokemonInfo = new PokemonInfo
            {
                Habitat = "other",
                IsLegendary = false,
            };

            // Act
            TranslationType translationType = translationStrategy.ResolveTranslationType(pokemonInfo);

            // Assert 
            Assert.AreEqual(TranslationType.Shakespeare, translationType);
        }
    }
}
