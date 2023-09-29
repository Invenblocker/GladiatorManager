using Contracts;

namespace Model
{
    public class Gladiator : IGladiator
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public IType Type { get; set; }
        public Dictionary<Stat, byte> PoolRemaining { get; set; }
        public Dictionary<Stat, byte> PoolMax { get; set; }
        public Dictionary<Stat, byte> Edge { get; set; }
        public Status Status { get; set; }
        public byte Effort { get; set; }
        public Armor Armor { get; set; }
        IArmor IGladiator.Armor { get { return Armor; } }
        public List<Skill> Skills { get; set; }
        List<ISkill> IGladiator.Skills
        {
            get
            {
                List<ISkill> skills = new List<ISkill>();
                foreach (Skill skill in Skills)
                {
                    skills.Add(skill);
                }
                return skills;
            }
        }
        public List<Ability> Abilities { get; set; }
        List<IAbility> IGladiator.Abilities
        {
            get
            {
                List<IAbility> abilities = new List<IAbility>();
                foreach(Ability ability in Abilities)
                {
                    abilities.Add(ability);
                }
                return abilities;
            }
        }
        public Weapon Weapon { get; set; }
        IWeapon IGladiator.Weapon { get { return Weapon; } }

        public bool Expend (Stat stat, byte baseCost, byte effort)
        {
            byte cost = CalculateCost(stat, baseCost, effort);
            if (cost < PoolRemaining[stat])
            {
                PoolRemaining[stat] -= cost;
                return true;
            }
            else
            {
                return false;
            }
        }
        public byte CalculateCost(Stat stat, byte baseCost, byte effort)
        {
            {
                byte effortCost = 2;
                Skill armorProf = Skills.Find(skill => skill.Name == "Armor Proficiency");
                if (armorProf == null || armorProf.Level < Armor.Weight)
                {
                    effortCost += Armor.Weight;
                }
                for(byte i = 0; i < effort; i++)
                {
                    if(i == 0)
                    {
                        baseCost += 1;
                    }
                    baseCost+= effortCost;
                }
                if(baseCost < Edge[stat])
                {
                    return 0;
                }
                else
                {
                    return (byte)(baseCost - Edge[stat]);
                }
            }
        }
        public byte MaxEffort(Stat stat, byte baseCost)
        {
            byte effortCost = 2;
            byte pool = PoolRemaining[stat];
            byte edge = Edge[stat];
            Skill armorProf = Skills.Find(skill => skill.Name == "Armor Proficiency");
            if (armorProf == null || armorProf.Level < Armor.Weight)
            {
                effortCost += Armor.Weight;
            }
            for(byte i  = 0; i < Effort; i++)
            {
                if(i == 0)
                {
                    pool -= 1;
                }
                else
                {
                    pool -= effortCost;
                }
                if(pool <= 0)
                {
                    return i;
                }
            }
            return Effort;
        }

        public byte FreeEffort(Stat stat, byte baseCost)
        {
            if (PoolRemaining[stat] == 0)
            {
                return 0;
            }
            else;
            {
                byte cost = 1;
                byte effortCost = 2;
                byte edge = Edge[stat];
                Skill armorProf = Skills.Find(skill => skill.Name == "Armor Proficiency");
                if (armorProf == null || armorProf.Level < Armor.Weight)
                {
                    effortCost += Armor.Weight;
                }
                for (byte i = 0; i < Effort; i++)
                {
                    cost += effortCost;
                    if (cost > edge)
                    {
                        return i;
                    }
                }
                return Effort;
            }
        }

        public byte GetDefense(Ability action, Stat defensiveStat, Stat targetStat, byte toHit, byte damage)
        {
            Skill evasionSkill = null;
            switch (defensiveStat)
            {
                case Stat.Might:
                    evasionSkill = Skills.Find(skill => skill.Name == "Might Defense");
                    break;
                case Stat.Speed:
                    evasionSkill = Skills.Find(skill => skill.Name == "Speed Defense");
                    break;
                case Stat.Intellect:
                    evasionSkill = Skills.Find(skill => skill.Name == "Intellect Defense");
                    break;
            }
            if (evasionSkill == null)
            {
                evasionSkill = new Skill("Untrained Defense", "Avoiding danger without training.", Stat.Speed, 0);
            }

            byte evasionValue = (byte)(evasionSkill.Level * 3);

            byte effort = FreeEffort(defensiveStat, 0);
            if (damage > PoolRemaining[targetStat] && Status == Status.Hale)
            {
                effort = MaxEffort(defensiveStat, 0);
            }
            if(effort > 0)
            {
                if (Expend(defensiveStat, 0, effort))
                {
                    evasionValue += (byte)(3 * effort);
                }
            }
            return (byte)(effort + Die.DieInstance.Roll(20));
        }
        public Status[] Damage(Stat target, byte value)
        {
            Status[] result = new Status[2];
            result[0] = Status;

            if (PoolRemaining[target] > value)
            {
                PoolRemaining[target] = (byte)(PoolRemaining[target] - value);
            }
            else
            {
                value -= PoolRemaining[target];
                PoolRemaining[target] = 0;
                Status = Status + 1;
                if(value > 0)
                {
                    if (PoolRemaining[Stat.Might] > 0)
                    {
                        Damage(Stat.Might, value);
                    }
                    else if (PoolRemaining[Stat.Speed] > 0)
                    {
                        Damage(Stat.Speed, value);
                    }
                    else if (PoolRemaining[Stat.Intellect] > 0)
                    {
                        Damage(Stat.Intellect, value);
                    }
                    else
                    {
                        Status = Status.Dead;
                    }
                }
            }

            result[1] = Status;
            return result;
        }
    }
}