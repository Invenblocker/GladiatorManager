using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFight
    {
        IGladiator[] Participants { get; }
        IPStatus Target { get; }
    }
}
