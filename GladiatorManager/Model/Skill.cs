using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Skill : ISkill
    {
        private string _name;
        private string _description;
        private Stat _stat;

        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        public Stat Stat { get { return _stat;} }
        public byte Level { get; set; }

        public Skill(string name, string description, Stat stat, byte level)
        {
            _name = name;
            _description = description;
            _stat = stat;
            Level = level;
        }
    }
}
