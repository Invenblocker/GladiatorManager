using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Weapon : IWeapon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Weight { get; set; }
        public bool IsRanged {  get; set; }
        public byte Damage { get; set; }
    }
}
