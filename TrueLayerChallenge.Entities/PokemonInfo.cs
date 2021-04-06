using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueLayerChallenge.Entities
{
    public class PokemonInfo
    {
        public static PokemonInfo Default { get; } = new PokemonInfo
        {
            Name = "Unknown",
            Description = "Unknown",
            Habitat = "Unknown",
            IsLegendary = false,
        };

        public string Name { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }
    }
}
