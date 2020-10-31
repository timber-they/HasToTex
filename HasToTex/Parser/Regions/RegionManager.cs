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
        public RegionManager (IEnumerable <(KeywordEnum? Start, KeywordEnum? End, char? Escape)> startEnds)
            => Regions = startEnds.Select (tuple => new Region (tuple.Start, tuple.End, tuple.Escape)).ToHashSet ();

        private HashSet <Region> Regions { get; }

        /// <summary>
        /// Registers the next character and evaluates the keyword responsible for a possible region change:
        /// <ul>
        /// <li>End: The keyword that ended the last region. It is returned, <i>after</i> the region has ended,
        /// i.e. once the region is not active anymore (at the last character of the end keyword)</li>
        /// <li>Start: The keyword that started the region. It is returned, <i>after</i> the region has started,
        /// i.e. once the region is active (at the last character of the start keyword)</li>
        /// </ul>
        /// </summary>
        /// <param name="c">The character to register</param>
        /// <returns>The keyword that ended / started a region, if this happened, null otherwise</returns>
        public KeywordEnum? Register (char c)
        {
            var active = Regions.FirstOrDefault (region => region.InRegion);
            if (active != null)
                return !active.Register (c) ? active.End : null;

            foreach (var region in Regions)
                region.Register (c);

            return Regions.FirstOrDefault (region => region.InRegion)?.Start;
        }

        public bool InRegion () => Regions.Any (region => region.InRegion);
    }
}