using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IType
    {
        public string Name { get; }
        public string[] Nicknames { get; }
        public Dictionary<Stat, byte> GenerateStartingPools();
        public Dictionary<Stat, byte> GenerateStartingEdge();
        public IAbility[] StartingAbilities { get; }
        public IAbility[][] TierAbilities { get; }
        public ISkill[] StartingSkills { get; }
    }
}
