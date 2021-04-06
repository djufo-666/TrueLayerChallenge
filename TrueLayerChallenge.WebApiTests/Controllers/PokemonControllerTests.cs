using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrueLayerChallenge.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace TrueLayerChallenge.WebApi.Controllers.Tests
{
    [TestClass]
    public class PokemonControllerTests
    {
        const string URI = "http://localhost:5000/pokemon/";
        HttpClient httpClient;

        [TestInitialize]
        public void Initialize()
        {
            httpClient = new HttpClient();
        }
        [TestCleanup]
        public void Cleanup()
        {
            httpClient.Dispose();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task Get_ByName_ShouldRespondWithPokemon()
        {
            // Arrange
            string name = "ditto";
            string url = $"{URI}{name}";

            // Act
            HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
            ViewModels.Pokemon pokemon = await responseMessage.Content.ReadFromJsonAsync<ViewModels.Pokemon>();

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.AreSame(name, pokemon.Name);
            Assert.IsNotNull(pokemon);
            Assert.IsNotNull(pokemon.Description);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetTranslated_ByName_ShouldRespondWithPokemon()
        {
            // Arrange
            string name = "ditto";
            string url = $"{URI}{name}";

            // Act
            HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
            ViewModels.Pokemon pokemon = await responseMessage.Content.ReadFromJsonAsync<ViewModels.Pokemon>();

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.AreSame(name, pokemon.Name);
            Assert.IsNotNull(pokemon);
            Assert.IsNotNull(pokemon.Description);
        }
    }

}