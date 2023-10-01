using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Warrior : Type
    {
        public override string Name { get { return "Warrior"; } }
        public override string[] Nicknames
        {
            get
            {
                string[] nicknames = { "the Crusher", "the Killer", "the Slasher", "the Bloodlusted", "the Merciless", "the Mighty", "the Bold", "the Strong", "the Veteran"};
                return nicknames;
            }
        }
        public override Dictionary<Stat, byte> GenerateStartingPools()
        {
            Dictionary<Stat, byte> pools = new Dictionary<Stat, byte>();
            pools.Add(Stat.Might, 10);
            pools.Add(Stat.Speed, 10);
            pools.Add(Stat.Intellect, 8);
            Stat[] distribution = { Stat.Might, Stat.Might, Stat.Might, Stat.Speed, Stat.Speed, Stat.Intellect };
            for(int i = 0; i < 6; i++)
            {
                Stat selection = distribution[Die.RollFromZero(distribution.Length)];
                pools[selection] = (byte)(pools[selection] + 1);
            }
            return pools;
        }
        public override Dictionary<Stat, byte> GenerateStartingEdge()
        {
            Dictionary<Stat, byte> edge = new Dictionary<Stat, byte>();
            if(Die.RollFromZero(6) < 2)
            {
                edge.Add(Stat.Might, 0);
                edge.Add(Stat.Speed, 1);
            }
            else
            {
                edge.Add(Stat.Might, 1);
                edge.Add(Stat.Speed, 0);
            }
            edge.Add(Stat.Intellect, 0);
            return edge;
        }
        public override List<Skill> StartingSkills
        {
            get
            {
                List<Skill> skills = new List<Skill>();
                skills.Add(new Skill("Armor Proficiency", "The maximum weight of armor that can be worn without penalty", Stat.Might, 2));
                skills.Add(new Skill("Light Weapon Proficiency", "The ability to use Light Weapons", Stat.Speed, 1));
                skills.Add(new Skill("Medium Weapon Proficiency", "The ability to use Medium Weapons", Stat.Might, 1));
                skills.Add(new Skill("Heavy Weapon Profiency", "The ability to use Heavy Weapons", Stat.Might, 1));
                return skills;
            }
        }

        public override IAbility[] StartingAbilities => throw new NotImplementedException();

        public override IAbility[][] TierAbilities => throw new NotImplementedException();
    }

}
