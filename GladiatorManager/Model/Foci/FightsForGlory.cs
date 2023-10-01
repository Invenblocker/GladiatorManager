using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FightsForGlory : Focus
    {
        public override string Name { get { return "Fights for GLory"; } }
        public override string[] Nicknames
        {
            get
            {
                string[] nicknames = { "the Victorious", "the Glorious", "the Famed", "the Accomplished" };
                return nicknames;
            }
        }

        public override List<Skill> StartingSkills
        {
            get
            {
                List<Skill> skills = new List<Skill>();
                skills.Add(new Skill("Medium Weapon Proficiency", "The ability to use Medium Weapons", Stat.Might, 1));
                return skills;
            }
        }

        public override List<Ability> StartingAbilities => throw new NotImplementedException();

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
            return pools;
        }
    }
}
