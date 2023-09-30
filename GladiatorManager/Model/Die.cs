using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Die
    {
        private static Random _random;
        private static Random Random
        {
            get
            {
                if( _random == null )
                {
                    _random = new Random();
                }
                return _random;
            }
        }

        public static byte Roll(byte sides)
        {
            return (byte)(Random.Next(sides) + 1);
        }
        public static byte RollFromZero(byte sides)
        {
            return (byte)(Random.Next(sides));
        }
        public static int RollFromZero(int max)
        {
            return(Random.Next(max));
        }
    }
}
