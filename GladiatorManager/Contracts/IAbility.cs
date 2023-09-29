using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAbility
    {
        public string Name { get; }
        public string Description { get; }
        public Stat Stat { get; }
        public byte Cost { get; }
        public IGladiator User { get; }
    }
}
