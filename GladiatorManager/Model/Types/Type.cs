using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Type : IType
    {
        public abstract string Name { get; }
        public abstract string[] Nicknames { get; }
        public abstract IAbility[] StartingAbilities { get; }
        public abstract IAbility[][] TierAbilities { get; }
        public abstract List<Skill> StartingSkills { get; }
        ISkill[] IType.StartingSkills
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
        public abstract Dictionary<Stat, byte> GenerateStartingEdge();
        public abstract Dictionary<Stat, byte> GenerateStartingPools();
    }
}
