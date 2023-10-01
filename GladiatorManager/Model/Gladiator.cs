using Contracts;
using System.Text;

namespace Model
{
    public class Gladiator : IGladiator
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string FullName
        {
            get
            {
                StringBuilder name = new StringBuilder();
                name.Append(FirstName);
                name.Append(' ');
                if(LastName != "")
                {
                    name.Append(LastName);
                    name.Append(' ');
                }
                name.Append(Nickname);
                return name.ToString();
            }
        }
        public string Description
        {
            get
            {
                StringBuilder description = new StringBuilder();
                description.Append(Descriptor.Name);
                description.Append(' ');
                description.Append(Type.Name);
                description.Append(" who ");
                description.Append(Focus.Name);
                return(description.ToString());
            }
        }
        public Gender Gender { get; set; }
        public IDescriptor Descriptor { get; set; }
        public Type Type { get; set; }
        IType IGladiator.Type { get { return Type; } }
        public IFocus Focus { get; set; }
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

        public Gladiator()
        {
            FirstName = null;
            LastName = null;
            Nickname = null;
            Gender = 0;
            Descriptor = null;
            Type = null;
            Focus = null;
            PoolRemaining = new Dictionary<Stat, byte>();
            PoolMax = new Dictionary<Stat, byte>();
            Edge = new Dictionary<Stat, byte>();
            Status = 0;
            Effort = 0;
            Armor = null;
            Skills = new List<Skill>();
            Abilities = new List<Ability>();
            Weapon = null;
        }

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
                if (stat == Stat.Speed && (armorProf == null || armorProf.Level < Armor.Weight))
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
            if (stat == Stat.Speed && (armorProf == null || armorProf.Level < Armor.Weight))
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
            else
            {
                baseCost += 1;
                byte effortCost = 2;
                byte edge = Edge[stat];
                Skill armorProf = Skills.Find(skill => skill.Name == "Armor Proficiency");
                if (stat == Stat.Speed && (armorProf == null || armorProf.Level < Armor.Weight))
                {
                    effortCost += Armor.Weight;
                }
                for (byte i = 0; i < Effort; i++)
                {
                    baseCost += effortCost;
                    if (baseCost > edge)
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
            return (byte)(effort + Die.Roll(20));
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

                Status = Status.Hale;
                foreach(Stat stat in PoolRemaining.Keys)
                {
                    if (PoolRemaining[stat] >= value)
                    {
                        PoolRemaining[stat] = (byte)(PoolRemaining[stat] - value);
                    }
                    else
                    {
                        value -= PoolRemaining[stat];
                        PoolRemaining[stat] = 0;
                    }
                    if (PoolRemaining[stat] == 0)
                    {
                        Status++;
                    }
                }
            }

            result[1] = Status;
            return result;
        }
        public string GenerateName()
        {
            string[] femaleNames = { "Trish", "Lady", "Mary", "Angela", "Tina", "Marie", "Linette", "Cerys", "Ashley", "Judith", "Erika", "Lissa", "Robin" };
            string[] maleNames = { "Dante", "Vergil", "Roy", "John", "Alexander", "Ashley", "Bernard", "Bob", "Tom", "Klaus", "Rex", "Zeke", "Noah", "Robin" };
            string[] nbNames = { "Ashley", "Robin", "Alex", "Nova", "Riley" };
            string[] lastNames = { "Lowen", "Glines", "Astora", "von Karma", "Wright", "Smith", "Jones", "Williams", "Brown", "White", "Garcia" };
            switch(Gender)
            {
                case Gender.Female:
                    FirstName = femaleNames[Die.RollFromZero(femaleNames.Length)];
                    break;
                case Gender.Male:
                    FirstName = maleNames[Die.RollFromZero(maleNames.Length)];
                    break;
                case Gender.Other:
                default:
                    FirstName = nbNames[Die.RollFromZero(nbNames.Length)];
                    break;
            }
            LastName = (Die.Roll(20) > 2) ? lastNames[Die.RollFromZero(lastNames.Length)] : "";
            if (Die.Roll(20) > 2 || LastName == "")
            {
                string[] baseNicknames = { "the Phoenix", "the Demon", "the Gambler", "the Merciless", "the Fabled", "the Legend", "the Beautiful", "the Envied", "the Reaper", "the Dandy", "the Grizzled" };
                List<string> nicknames = new List<string>();
                nicknames.AddRange(baseNicknames);
                nicknames.AddRange(Descriptor.Nicknames);
                nicknames.AddRange(Type.Nicknames);
                nicknames.AddRange(Focus.Nicknames);
                Nickname = nicknames[Die.RollFromZero(nicknames.Count())];
            }
            return (FullName);
        }

        public string PerformTurn(IEnumerable<Gladiator> other)
        {
            Gladiator[] others = other.ToArray();
            if (others.Length > 0)
            {
                Gladiator target = others[Die.RollFromZero(others.Length)];
                Ability action = Abilities[0];

                byte effort = FreeEffort(action.Stat, action.Cost);
                Expend(action.Stat, action.Cost, effort);

                return action.Use(target, effort, 0, 0);
            }
            else
            {
                return (FirstName + " has no target, and skips turn.");
            }
        }
    }
}