using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueLayerChallenge.Entities.PokeApi
{
    public class PokemonSpecies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLegendary { get; set; }
        public NamedAPIResource Habitat { get; set; }
        public FlavorTextEntry[] FlavorTextEntries { get; set; }
    }
}
