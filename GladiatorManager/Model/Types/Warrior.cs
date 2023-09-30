using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Warrior : IType
    {
        public string Name { get { return "Warrior"; } }
        public string[] Nicknames
        {
            get
            {
                string[] nicknames = { "the Crusher", "the Killer", "the Slasher", "the Bloodlusted", "the Merciless", "the Mighty", "the Bold", "the Strong", "the Veteran"};
                return nicknames;
            }
        }
        public Dictionary<Stat, byte> GenerateStartingPools()
        {
            Dictionary<Stat, byte> pools = new Dictionary<Stat, byte>();
            pools.Add(Stat.Might, 10);
            pools.Add(Stat.Speed, 10);
            pools.Add(Stat.Intellect, 8);
            Stat[] distribution = { Stat.Might, Stat.Might, Stat.Might, Stat.Speed, Stat.Speed, Stat.Intellect };
            for(int i = 0; i < 6; i++)
            {
                Stat selection = distribution[Die.RollFromZero(distribution.Length)];
                pools[selection] = (byte)(pools[selection] + 1);
            }
            return pools;
        }
        public Dictionary<Stat, byte> GenerateStartingEdge()
        {
            Dictionary<Stat, byte> edge = new Dictionary<Stat, byte>();
            if(Die.RollFromZero(6) < 2)
            {
                edge.Add(Stat.Might, 0);
                edge.Add(Stat.Speed, 1);
            }
            else
            {
                edge.Add(Stat.Might, 1);
                edge.Add(Stat.Speed, 0);
            }
            edge.Add(Stat.Intellect, 0);
            return edge;
        }
    }

}
