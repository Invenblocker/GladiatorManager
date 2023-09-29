using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BasicStrike : Ability
    {
        public override string Name { get { return "Strike"; } }
        public override string Description { get { return "Deals " + User.Weapon.Damage + " to target."; } }
        public override Stat Stat { get { return User.Weapon.IsRanged ? Stat.Speed : Stat.Might; } }
        public override byte Cost { get { return 0; } }

        public BasicStrike(Gladiator user) : base(user, true, true, false) { }
        
        public override string Use(Gladiator target, byte powerEffort, byte precissionEffort, byte otherEffort)
        {
            Skill weaponSkill = null;
            byte toHit = 0;
            byte damage = 0;
            switch(User.Weapon.Weight)
            {
                case 1:
                    weaponSkill = User.Skills.Find(skill => skill.Name == "Light Weapon Proficiency");
                    toHit += 3;
                    damage = 2;
                    break;
                case 2:
                    weaponSkill = User.Skills.Find(skill => skill.Name == "Medium Weapon Proficiency");
                    damage = 4;
                    break;
                case 3:
                    weaponSkill = User.Skills.Find(skill => skill.Name == "Heavy Weapon Proficiency");
                    damage = 6;
                    break;
            }
            if(weaponSkill == null)
            {
                weaponSkill = new Skill("Untrained", "Using a weapon without training.", Stat, 0);
            }
            toHit += (byte)(weaponSkill.Level * 3 + Die.DieInstance.Roll(20));
            damage += (byte)(damage + 2 * powerEffort);
            damage -= target.Armor.Value;
            byte evasion = target.GetDefense(this, Stat.Speed, Stat.Might, toHit, damage);
            if(toHit > evasion)
            {
                Status[] targetStatus = target.Damage(Stat.Might, damage);
                if (targetStatus[0] == targetStatus[1])
                {
                    return (User.FirstName + " attacks " + target.FirstName + ", and deals " + damage + " damage.");
                }
                else
                {
                    switch(targetStatus[1])
                    {
                        case Status.Impaired:
                            return (User.FirstName + " attacks " + target.FirstName + ", and deals " + damage + " damage and impairs them.");
                        case Status.Debilitated:
                            return (User.FirstName + " attacks " + target.FirstName + ", and deals " + damage + " damage and debilitates them.");
                        case Status.Dead:
                            return (User.FirstName + " attacks " + target.FirstName + ", and deals " + damage + " damage and kills them.");
                    }
                }
            }
            else
            {
                return (User.FirstName + " attacks " + target.FirstName + ", but misses.");
            }
        }
    }
}
