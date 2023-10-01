using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Model;

namespace ViewModel
{
    internal class GladiatorGenerator
    {
        public static Gladiator Generate()
        {
            Gladiator gladiator = new Gladiator();
            gladiator.Descriptor = new Strong();
            gladiator.Type = new Warrior();
            gladiator.Focus = new FightsForGlory();
            byte genderRoll = Die.Roll(20);
            if (genderRoll <= 4)
            {
                gladiator.Gender = Gender.Female;
            }
            else if (genderRoll <= 19)
            {
                gladiator.Gender = Gender.Male;
            }
            else
            {
                gladiator.Gender = Gender.Other;
            }
            gladiator.GenerateName();

            gladiator.PoolMax = gladiator.Type.GenerateStartingPools();
            gladiator.Edge = gladiator.Type.GenerateStartingEdge();
            Dictionary<Stat, byte> focusPools = gladiator.Focus.GenerateStartingPools(), focusEdge = gladiator.Focus.GenerateStartingEdge(),
                descriptorPools = gladiator.Descriptor.GenerateStartingPools(), descriptorEdge = gladiator.Descriptor.GenerateStartingEdge();
            foreach (Stat stat in focusPools.Keys)
            {
                gladiator.PoolMax[stat] = (byte)(gladiator.PoolMax[stat] + focusPools[stat]);
            }
            foreach (Stat stat in focusEdge.Keys)
            {
                gladiator.Edge[stat] = (byte)(gladiator.Edge[stat] + focusEdge[stat]);
            }
            foreach (Stat stat in descriptorPools.Keys)
            {
                gladiator.PoolMax[stat] = (byte)(gladiator.PoolMax[stat] + descriptorPools[stat]);
            }
            foreach (Stat stat in descriptorEdge.Keys)
            {
                gladiator.Edge[stat] = (byte)(gladiator.Edge[stat] + descriptorEdge[stat]);
            }

            gladiator.Effort = 1;

            gladiator.Abilities.Add(new BasicStrike(gladiator));

            gladiator.Skills.AddRange(gladiator.Type.StartingSkills);
            foreach(Skill skill in gladiator.Descriptor.SkillModifiers)
            {
                Skill currentSkill = gladiator.Skills.Find(x => x.Name == skill.Name);
                if(currentSkill != null)
                {
                    currentSkill.Level = (byte)(currentSkill.Level + skill.Level);
                }
                else
                {
                    gladiator.Skills.Add(skill);
                }
            }
            foreach (Skill skill in gladiator.Focus.StartingSkills)
            {
                Skill currentSkill = gladiator.Skills.Find(x => x.Name == skill.Name);
                if (currentSkill != null)
                {
                    currentSkill.Level = (byte)(currentSkill.Level + skill.Level);
                }
                else
                {
                    gladiator.Skills.Add(skill);
                }
            }

            byte armorProf = (gladiator.Skills.Find(skill => skill.Name == "Armor Proficiency") ?? new Skill ("Unarmored Defense", "How are you even reading this?", Stat.Might, 0)).Level;
            switch (armorProf)
            {
                case 0:
                    if(Die.RollFromZero(5) == 0)
                    {
                        switch(Die.Roll(6))
                        {
                            case 1: case 2: case 3:
                                gladiator.Armor = new Armor { Name = "Light Armor", Description = "Not much, but better than nothing", Value = 1, Weight = 1 };
                                break;
                            case 4: case 5:
                                gladiator.Armor = new Armor { Name = "Medium Armor", Description = "Rudimentary Armor", Value = 2, Weight = 2};
                                break;
                            case 6:
                                gladiator.Armor = new Armor { Name = "Heavy Armor", Description = "You can barely see the gladiator for the plate", Value = 3, Weight = 3 };
                                break;
                        }
                    }
                    break;
                case 1:
                    if(Die.RollFromZero (5) == 0)
                    {
                        switch (Die.Roll(5))
                        {
                            case 1:
                            case 2:
                                break;
                            case 3:
                            case 4:
                                gladiator.Armor = new Armor { Name = "Medium Armor", Description = "Rudimentary Armor", Value = 2, Weight = 2 };
                                break;
                            case 5:
                                gladiator.Armor = new Armor { Name = "Heavy Armor", Description = "You can barely see the gladiator for the plate", Value = 3, Weight = 3 };
                                break;
                        }
                    }
                    else
                    {
                        gladiator.Armor = new Armor { Name = "Light Armor", Description = "Not much, but better than nothing", Value = 1, Weight = 1 };
                    }
                    break;
                case 2:
                    if (Die.RollFromZero(5) == 0)
                    {
                        switch (Die.Roll(5))
                        {
                            case 1:
                                break;
                            case 2:
                            case 3:
                                gladiator.Armor = new Armor { Name = "Light Armor", Description = "Not much, but better than nothing", Value = 1, Weight = 1 };
                                break;
                            case 4:
                            case 5:
                                gladiator.Armor = new Armor { Name = "Heavy Armor", Description = "You can barely see the gladiator for the plate", Value = 3, Weight = 3 };
                                break;
                        }
                    }
                    else
                    {
                        gladiator.Armor = new Armor { Name = "Medium Armor", Description = "Rudimentary Armor", Value = 2, Weight = 2 };
                    }
                    break;
                case 3:
                    if (Die.RollFromZero(5) == 0)
                    {
                        switch (Die.Roll(6))
                        {
                            case 1:
                                break;
                            case 2:
                            case 3:
                                gladiator.Armor = new Armor { Name = "Light Armor", Description = "Not much, but better than nothing", Value = 1, Weight = 1 };
                                break;
                            case 4:
                            case 5:
                            case 6:
                                gladiator.Armor = new Armor { Name = "Medium Armor", Description = "Rudimentary Armor", Value = 2, Weight = 2 };
                                break;
                        }
                    }
                    else
                    {
                        gladiator.Armor = new Armor { Name = "Heavy Armor", Description = "You can barely see the gladiator for the plate", Value = 3, Weight = 3 };
                    }
                    break;
            }

            List<Weapon> weaponPool = new List<Weapon>();
            weaponPool.Add(new Weapon { Name = "Knife", Description = "It's a sharp metal bit.", Damage = 2, IsRanged = false, Weight = 1 });
            weaponPool.Add(new Weapon { Name = "Sword", Description = "It's a sharp metal stick.", Damage = 4, IsRanged = false, Weight = 2 });
            weaponPool.Add(new Weapon { Name = "Greatsword", Description = "It's big, it's sharp, and it's metal.", Damage = 6, IsRanged = false, Weight = 3 });
            weaponPool.Add(new Weapon { Name = "Bow", Description = "It launches pointy sticks at people.", Damage = 6, IsRanged = true, Weight = 3 });
            for(int i = (gladiator.Skills.Find(skill => skill.Name == "Light Weapon Proficiency") ?? new Skill("", "", Stat.Speed, 0)).Level; i >0; i--)
            {
                weaponPool.Add(new Weapon { Name = "Knife", Description = "It's a sharp metal bit.", Damage = 2, IsRanged = false, Weight = 1 });
                weaponPool.Add(new Weapon { Name = "Knife", Description = "It's a sharp metal bit.", Damage = 2, IsRanged = false, Weight = 1 });
            }
            for (int i = (gladiator.Skills.Find(skill => skill.Name == "Medium Weapon Proficiency") ?? new Skill("", "", Stat.Speed, 0)).Level; i > 0; i--)
            {
                weaponPool.Add(new Weapon { Name = "Sword", Description = "It's a sharp metal stick.", Damage = 4, IsRanged = false, Weight = 2 });
                weaponPool.Add(new Weapon { Name = "Sword", Description = "It's a sharp metal stick.", Damage = 4, IsRanged = false, Weight = 2 });
            }
            for (int i = (gladiator.Skills.Find(skill => skill.Name == "Heavy Weapon Proficiency") ?? new Skill("", "", Stat.Speed, 0)).Level; i > 0; i--)
            {
                weaponPool.Add(new Weapon { Name = "Greatsword", Description = "It's big, it's sharp, and it's metal.", Damage = 6, IsRanged = false, Weight = 3 });
                weaponPool.Add(new Weapon { Name = "Bow", Description = "It launches pointy sticks at people.", Damage = 6, IsRanged = true, Weight = 3 });
            }
            gladiator.Weapon = weaponPool[Die.RollFromZero(weaponPool.Count())];

            return gladiator;
        }

        public static Gladiator[] GenerateMultiple(int amount)
        {
            Gladiator[] gladiators = new Gladiator[amount];
            for(int i = 0; i < amount; i++)
            {
                gladiators[i] = Generate();
            }
            return gladiators;
        }
    }
}
