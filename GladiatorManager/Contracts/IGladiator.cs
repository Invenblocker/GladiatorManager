using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGladiator
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Nickname { get; }
        public IType Type { get; }
        public Dictionary<Stat, byte> PoolRemaining { get; }
        public Dictionary<Stat, byte> PoolMax { get; }
        public Dictionary<Stat, byte> Edge { get; }
        public Status Status { get; }
        public byte Effort { get; }
        public IArmor Armor { get; }
        public IWeapon Weapon { get; }
        public List<ISkill> Skills { get; }
        public List<IAbility> Abilities { get; }
    }
}
