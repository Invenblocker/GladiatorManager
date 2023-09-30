using Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    internal class GladiatorAnalysis
    {
        public static void PrintAnalysis(Gladiator gladiator)
        {
            Console.WriteLine(gladiator.FullName + " is a " + gladiator.Description);
            Console.WriteLine("Might Pool: " + gladiator.PoolMax[Stat.Might] + ", Speed Pool: " + gladiator.PoolMax[Stat.Speed] + ", Intellect Pool: " + gladiator.PoolMax[Stat.Intellect]);
            Console.WriteLine("Might Edge: " + gladiator.Edge[Stat.Might] + ", Speed Edge: " + gladiator.Edge[Stat.Speed] + ", Intellect Edge: " + gladiator.Edge[Stat.Intellect]);
        }
    }
}
