using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Armor : IArmor
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Value { get; set; }
        public byte Weight { get; set; }
    }
}
