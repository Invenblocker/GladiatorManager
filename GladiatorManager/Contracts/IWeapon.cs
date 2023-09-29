using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWeapon
    {
        public string Name { get; }
        public string Description { get; }
        public byte Weight { get; }
        public bool IsRanged { get; }
        public byte Damage { get; }
    }
}
