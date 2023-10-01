using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Strong : Descriptor
    {
        public override string Name { get { return "Strong"; } }

        public override string[] Nicknames
        {
            get
            {
                string[] nicknames = { "the Mighty", "the Strong", "the Powerful", "the Muscled"};
                return nicknames;
            }
        }
        public override List<Skill> SkillModifiers
        {
            get
            {
                List<Skill> skills = new List<Skill>();
                skills.Add(new Skill("Armor Proficiency", "The ability to use armor", Stat.Might, 1));
                skills.Add(new Skill("Might Defense", "Using Might to defend oneself from harm", Stat.Might, 1));
                return skills;
            }
        }

        public override Dictionary<Stat, byte> GenerateStartingEdge()
        {
            Dictionary<Stat, byte> edge = new Dictionary<Stat, byte>();
            edge.Add(Stat.Might, 1);
            return edge;
        }

        public override Dictionary<Stat, byte> GenerateStartingPools()
        {
            Dictionary<Stat, byte> pools = new Dictionary<Stat, byte>();
            pools.Add(Stat.Might, 4);
            return (pools);
        }
    }
}
