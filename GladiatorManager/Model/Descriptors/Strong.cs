using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Strong : IDescriptor
    {
        public string Name { get { return "Strong"; } }

        public string[] Nicknames
        {
            get
            {
                string[] nicknames = { "the Mighty", "the Strong", "the Powerful", "the Muscled"};
                return nicknames;
            }
        }

        public Dictionary<Stat, byte> GenerateStartingEdge()
        {
            return (new Dictionary<Stat, byte>());
        }

        public Dictionary<Stat, byte> GenerateStartingPools()
        {
            Dictionary<Stat, byte> pools = new Dictionary<Stat, byte>();
            pools.Add(Stat.Might, 4);
            return (pools);
        }
    }
}
