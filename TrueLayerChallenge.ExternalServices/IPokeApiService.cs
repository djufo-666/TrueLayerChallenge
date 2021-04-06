using System.Threading.Tasks;
using TrueLayerChallenge.Entities.PokeApi;

namespace TrueLayerChallenge.ExternalServices
{
    public interface IPokeApiService
    {
        Task<PokemonSpecies> GetByNameAsync(string name);
    }
}