using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFocus
    {
        public string Name { get; }
        public string[] Nicknames { get; }
        public Dictionary<Stat, byte> GenerateStartingPools();
        public Dictionary<Stat, byte> GenerateStartingEdge();
    }
}
