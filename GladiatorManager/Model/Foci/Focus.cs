using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Focus : IFocus
    {
        public abstract string Name { get; }
        public abstract string[] Nicknames { get; }
        public abstract List<Skill> StartingSkills { get; }
        ISkill[] IFocus.StartingSkills
        {
            get
            {
                ISkill[] skills = new ISkill[StartingSkills.Count()];
                for(int i = 0; i < skills.Length; i++)
                {
                    skills[i] = StartingSkills[i];
                }
                return skills;
            }
        }
        public abstract List<Ability> StartingAbilities { get; }
        IAbility[] IFocus.StartingAbilities
        {
            get
            {
                IAbility[] abilities = new IAbility[StartingAbilities.Count()];
                for (int i = 0; i < abilities.Length; i++)
                {
                    abilities[i] = StartingAbilities[i];
                }
                return abilities;
            }
        }

        public abstract Dictionary<Stat, byte> GenerateStartingEdge();
        public abstract Dictionary<Stat, byte> GenerateStartingPools();
    }
}
