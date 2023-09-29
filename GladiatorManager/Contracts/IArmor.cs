using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IArmor
    {
        string Name { get; }
        string Description { get; }
        byte Value { get; }
        byte Weight { get; }
    }
}
