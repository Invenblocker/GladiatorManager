using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FightsForGlory : IFocus
    {
        public string Name { get { return "Fights for GLory"; } }
        public string[] Nicknames
        {
            get
            {
                string[] nicknames = { "the Victorious", "the Glorious", "the Famed", "the Accomplished" };
                return nicknames;
            }
        }

        public Dictionary<Stat, byte> GenerateStartingEdge()
        {
            Dictionary<Stat, byte> edge = new Dictionary<Stat, byte>();
            edge.Add(Stat.Might, 1);
            return edge;
        }

        public Dictionary<Stat, byte> GenerateStartingPools()
        {
            Dictionary<Stat, byte> pools = new Dictionary<Stat, byte>();
            pools.Add(Stat.Might, 4);
            return pools;
        }
    }
}
