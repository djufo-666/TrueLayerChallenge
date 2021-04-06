using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueLayerChallenge.Services
{
    public class TranslationStrategy : ITranslationStrategy
    {
        /// <summary>
        /// 1.‌ ‌If‌ ‌the‌ ‌Pokemon’s‌ ‌habitat‌ ‌is‌ ‌‌cave‌‌ ‌or‌ ‌it’s‌ ‌a‌ ‌legendary‌ ‌Pokemon‌ ‌then‌ ‌apply‌ ‌the‌ ‌Yoda‌ ‌translation.‌ ‌
        /// 2.‌ ‌For‌ ‌all‌ ‌other‌ ‌Pokemon,‌ ‌apply‌ ‌the‌ ‌Shakespeare‌ ‌translation.‌ ‌
        /// 3.‌ ‌If‌ ‌you‌ ‌can’t‌ ‌translate‌ ‌the‌ ‌Pokemon’s‌ ‌description‌ ‌(for‌ ‌whatever‌ ‌reason‌ ‌😉)‌ ‌then‌ ‌use‌ ‌the‌ ‌
        /// standard‌ ‌description‌
        /// </summary>
        /// <param name="pokemonInfo"></param>
        /// <returns></returns>
        public Entities.TranslationType ResolveTranslationType(Entities.PokemonInfo pokemonInfo) => pokemonInfo switch
        {
            { Habitat: "cave" } => Entities.TranslationType.Yoda,
            { IsLegendary: true } => Entities.TranslationType.Yoda,
            _ => Entities.TranslationType.Shakespeare
        };
    }
}
