using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Descriptor : IDescriptor
    {
        public abstract string Name { get; }
        public abstract string[] Nicknames { get; }
        public abstract List<Skill> SkillModifiers { get; }
        ISkill[] IDescriptor.SkillModifiers
        {
            get
            {
                ISkill[] skillModifiers = new ISkill[SkillModifiers.Count()];
                for(int i = 0; i < skillModifiers.Length; i++)
                {
                    skillModifiers[i] = SkillModifiers[i];
                }
                return skillModifiers;
            }
        }

        public abstract Dictionary<Stat, byte> GenerateStartingEdge();
        public abstract Dictionary<Stat, byte> GenerateStartingPools();
    }
}
