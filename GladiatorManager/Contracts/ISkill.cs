using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISkill
    {
        public string Name { get; }
        public string Description { get; }
        public Stat Stat { get; }
        public byte Level { get; }
    }
}
