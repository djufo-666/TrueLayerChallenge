using TrueLayerChallenge.Entities;

namespace TrueLayerChallenge.Services
{
    public interface ITranslationStrategy
    {
        TranslationType ResolveTranslationType(PokemonInfo pokemonInfo);
    }
}