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
            gladiator.Abilities.Add(new BasicStrike(gladiator));
            gladiator.Weapon = new Weapon { Name = "Sword", Description = "It's a sharp metal stick.", Damage = 4, IsRanged = false, Weight = 2 };
            gladiator.Armor = new Armor { Name = "None", Description = "Just clothes, no armor", Value = 0, Weight = 0 };

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
