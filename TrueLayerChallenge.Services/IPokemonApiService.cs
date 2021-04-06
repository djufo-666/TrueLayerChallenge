using System.Threading.Tasks;
using TrueLayerChallenge.Entities;

namespace TrueLayerChallenge.Services
{
    public interface IPokemonApiService
    {
        Task<PokemonInfo> GetByNameAsync(string name);
        Task<PokemonInfo> GetByNameTranslatedAsync(string name);
    }
}