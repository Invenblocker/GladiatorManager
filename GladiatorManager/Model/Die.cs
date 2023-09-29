using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Die
    {
        private static Die _die;
        private Random random;
        public static Die DieInstance
        {
            get
            {
                if (_die == null)
                {
                    _die = new Die();
                }
                return _die;
            }
        }

        private Die()
        {
            random = new Random();
        }

        public byte Roll(byte sides)
        {
            return (byte)(random.Next(sides) + 1);
        }

        public byte RollFromZero(byte sides)
        {
            return (byte)(random.Next(sides));
        }
    }
}
