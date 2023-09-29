using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Fight : IFight
    {
        private Gladiator[] _participants; 

        public Fight(Gladiator leftSeat, Gladiator rightSeat, Status target)
        {

        }
    }
}
