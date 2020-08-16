using System.Collections;
using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell.Keywords;


namespace HasToTex.Parser.Regions
{
    public class RegionManager
    {
        public RegionManager (HashSet <Region> regions) => Regions = regions;

        /// <summary>
        /// Initializes a new region manager
        /// </summary>
        /// <param name="startEnds">The starts and ends of the regions. Null stands for \n</param>
        public RegionManager (IEnumerable <(KeywordEnum? Start, KeywordEnum? End)> startEnds)
            => Regions = startEnds.Select (tuple => new Region (tuple.Start, tuple.End)).ToHashSet ();

        private HashSet <Region> Regions { get; }

        /// <summary>
        /// Registers the next character
        /// </summary>
        /// <param name="c">The character to register</param>
        /// <param name="current">The current string (with the character at the end)</param>
        /// <returns>The keyword that ended / started a region, if this happened, null otherwise</returns>
        public KeywordEnum? Register (char c, string current)
        {
            var active = Regions.FirstOrDefault (region => region.InRegion);
            if (active != null)
            {
                active.Register (c, current);
                return !active.InRegion ? active.End : null;
            }

            foreach (var region in Regions)
                region.Register (c, current);

            return Regions.FirstOrDefault (region => region.InRegion)?.Start;
        }

        public bool InRegion () => Regions.Any (region => region.InRegion);
    }
}